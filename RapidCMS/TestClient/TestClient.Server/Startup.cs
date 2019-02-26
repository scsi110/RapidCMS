﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RapidCMS.Common.Enums;
using RapidCMS.Common.Extensions;
using RapidCMS.Common.Interfaces;
using RapidCMS.Common.Models;
using RapidCMS.Common.Models.Config;
using TestLibrary;

namespace TestClient.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var hacky = new RapidCMS.Common.Startup();

            services.AddSingleton<RepositoryA>();
            services.AddSingleton<RepositoryB>();
            services.AddSingleton<RepositoryC>();
            services.AddSingleton<RepositoryD>();
            services.AddSingleton<RepositoryE>();
            services.AddSingleton<RepositoryF>();
            services.AddSingleton<VariantRepository>();

            void listView(ListViewConfig<TestEntity> listViewConfig)
            {
                listViewConfig
                    .AddDefaultButton(DefaultButtonType.New, "New")
                    .AddListPane(pane =>
                    {
                        pane.AddProperty(x => x.Id);
                        pane.AddProperty(x => x.Name).SetDescription("This is a name");
                        pane.AddProperty(x => x.Description).SetDescription("This is a description");
                        pane.AddDefaultButton(DefaultButtonType.View, string.Empty);
                        pane.AddDefaultButton(DefaultButtonType.Edit, string.Empty);

                    });
            }

            void nodeEditor(NodeEditorConfig<TestEntity> nodeEditorConfig)
            {
                nodeEditorConfig
                    .AddDefaultButton(DefaultButtonType.SaveNew)
                    .AddDefaultButton(DefaultButtonType.SaveExisting)
                    .AddDefaultButton(DefaultButtonType.Delete)
                    .AddCustomButton("create-button", () => { }, "Custom create!")
                    .AddEditorPane(pane =>
                    {
                        pane.AddField(x => x.Id)
                            .SetValueMapper(new IntValueMapper())
                            .SetReadonly(true);

                        pane.AddField(x => x.Name)
                            .SetDescription("This is a name");

                        pane.AddField(x => x.Description)
                            .SetDescription("This is a description")
                            .SetType(EditorType.TextArea);

                        pane.AddField(x => x.Number)
                            .SetDescription("This is a number")
                            .SetValueMapper(new IntValueMapper())
                            .SetType(EditorType.Numeric);
                    });
            }

            void listNodeEditor(ListEditorConfig<TestEntity> listEditorConfig)
            {
                listEditorConfig.AddDefaultButton(DefaultButtonType.New);
                listEditorConfig.AddEditor(editor =>
                {
                    editor.AddDefaultButton(DefaultButtonType.SaveNew);
                    editor.AddDefaultButton(DefaultButtonType.View);
                    editor.AddDefaultButton(DefaultButtonType.Edit);
                    editor.AddDefaultButton(DefaultButtonType.SaveExisting);
                    editor.AddDefaultButton(DefaultButtonType.Delete);

                    editor.AddField(x => x.Id)
                        .SetDescription("This should be readonly")
                        .SetReadonly();

                    editor.AddField(x => x.Name)
                        .SetDescription("This is a name");

                    editor.AddField(x => x.Description)
                        .SetDescription("This is a description")
                        .SetType(EditorType.TextArea);

                    editor.AddField(x => x.Number)
                        .SetDescription("This is a number")
                        .SetValueMapper(new IntValueMapper())
                        .SetType(EditorType.Numeric);
                });
            }

            void nodeEditorWithSubCollection(NodeEditorConfig<TestEntity> nodeEditorConfig)
            {
                nodeEditorConfig
                    .AddDefaultButton(DefaultButtonType.SaveNew)
                    .AddDefaultButton(DefaultButtonType.SaveExisting)
                    .AddDefaultButton(DefaultButtonType.Delete)

                    .AddEditorPane(pane =>
                    {
                        pane.AddField(x => x.Id)
                            .SetValueMapper(new IntValueMapper())
                            .SetReadonly(true);

                        pane.AddField(x => x.Name)
                            .SetDescription("This is a name");

                        pane.AddField(x => x.Description)
                            .SetDescription("This is a description")
                            .SetType(EditorType.TextArea);

                        pane.AddField(x => x.Number)
                            .SetDescription("This is a number")
                            .SetValueMapper(new IntValueMapper())
                            .SetType(EditorType.Numeric);
                    })
                    
                    .AddEditorPane(pane =>
                    {
                        pane.AddSubCollectionListEditor("sub-collection-1", subCollectionListNodeEditor);
                    });
            }

            void nodeEditorWithPolymorphicSubCollection(NodeEditorConfig<TestEntity> nodeEditorConfig)
            {
                nodeEditorConfig
                    .AddDefaultButton(DefaultButtonType.SaveNew)
                    .AddDefaultButton(DefaultButtonType.SaveExisting)
                    .AddDefaultButton(DefaultButtonType.Delete)

                    .AddEditorPane(pane =>
                    {
                        pane.AddField(x => x.Id)
                            .SetValueMapper(new IntValueMapper())
                            .SetReadonly(true);

                        pane.AddField(x => x.Name)
                            .SetDescription("This is a name");

                        pane.AddField(x => x.Description)
                            .SetDescription("This is a description")
                            .SetType(EditorType.TextArea);

                        pane.AddField(x => x.Number)
                            .SetDescription("This is a number")
                            .SetValueMapper(new IntValueMapper())
                            .SetType(EditorType.Numeric);
                    })

                    .AddEditorPane(pane =>
                    {
                        pane.AddSubCollectionListEditor("sub-collection-3", subCollectionListNodeEditor);
                    });
            }

            void nodeEditorWithPolymorphism(NodeEditorConfig<TestEntity> nodeEditorConfig)
            {
                nodeEditorConfig
                    .AddDefaultButton(DefaultButtonType.SaveNew)
                    .AddDefaultButton(DefaultButtonType.SaveExisting)
                    .AddDefaultButton(DefaultButtonType.Delete)

                    .AddEditorPane<TestEntityVariantA>(pane =>
                    {
                        pane.AddField(x => x.Id)
                            .SetValueMapper(new IntValueMapper())
                            .SetReadonly(true);

                        pane.AddField(x => x.Name)
                            .SetDescription("This is a name");

                        pane.AddField(x => x.Description)
                            .SetDescription("This is a description")
                            .SetType(EditorType.TextArea);

                        pane.AddField(x => x.Number)
                            .SetDescription("This is a number")
                            .SetValueMapper(new IntValueMapper())
                            .SetType(EditorType.Numeric);

                        pane.AddField(x => x.Title)
                            .SetDescription("This is a title");
                    })

                    .AddEditorPane<TestEntityVariantB>(pane =>
                    {
                        pane.AddField(x => x.Id)
                            .SetValueMapper(new IntValueMapper())
                            .SetReadonly(true);

                        pane.AddField(x => x.Name)
                            .SetDescription("This is a name");

                        pane.AddField(x => x.Description)
                            .SetDescription("This is a description")
                            .SetType(EditorType.TextArea);

                        pane.AddField(x => x.Number)
                            .SetDescription("This is a number")
                            .SetValueMapper(new IntValueMapper())
                            .SetType(EditorType.Numeric);

                        pane.AddField(x => x.Image)
                            .SetDescription("This is an image");
                    })
                
                    .AddEditorPane<TestEntityVariantC>(pane =>
                     {
                         pane.AddField(x => x.Id)
                             .SetValueMapper(new IntValueMapper())
                             .SetReadonly(true);

                         pane.AddField(x => x.Name)
                             .SetDescription("This is a name");

                         pane.AddField(x => x.Description)
                             .SetDescription("This is a description")
                             .SetType(EditorType.TextArea);

                         pane.AddField(x => x.Number)
                             .SetDescription("This is a number")
                             .SetValueMapper(new IntValueMapper())
                             .SetType(EditorType.Numeric);

                         pane.AddField(x => x.Quote)
                             .SetDescription("This is a quote");
                     });
            }

            void listNodeEditorWithPolymorphism(ListEditorConfig<TestEntity> listEditorConfig)
            {
                listEditorConfig.AddDefaultButton(DefaultButtonType.New);
                listEditorConfig.AddEditor<TestEntityVariantA>(editor =>
                {
                    editor.AddDefaultButton(DefaultButtonType.SaveNew);
                    editor.AddDefaultButton(DefaultButtonType.View);
                    editor.AddDefaultButton(DefaultButtonType.Edit);
                    editor.AddDefaultButton(DefaultButtonType.SaveExisting);
                    editor.AddDefaultButton(DefaultButtonType.Delete);

                    editor.AddField(x => x.Id)
                        .SetDescription("This should be readonly")
                        .SetReadonly();

                    editor.AddField(x => x.Name)
                        .SetDescription("This is a name");

                    editor.AddField(x => x.Description)
                        .SetDescription("This is a description")
                        .SetType(EditorType.TextArea);

                    editor.AddField(x => x.Number)
                        .SetDescription("This is a number")
                        .SetValueMapper(new IntValueMapper())
                        .SetType(EditorType.Numeric);

                    editor.AddField(x => x.Title)
                        .SetDescription("This is a title");
                });

                listEditorConfig.AddEditor<TestEntityVariantB>(editor =>
                {
                    editor.AddDefaultButton(DefaultButtonType.SaveNew);
                    editor.AddDefaultButton(DefaultButtonType.View);
                    editor.AddDefaultButton(DefaultButtonType.Edit);
                    editor.AddDefaultButton(DefaultButtonType.SaveExisting);
                    editor.AddDefaultButton(DefaultButtonType.Delete);

                    editor.AddField(x => x.Id)
                        .SetDescription("This should be readonly")
                        .SetReadonly();

                    editor.AddField(x => x.Name)
                        .SetDescription("This is a name");

                    editor.AddField(x => x.Description)
                        .SetDescription("This is a description")
                        .SetType(EditorType.TextArea);

                    editor.AddField(x => x.Number)
                        .SetDescription("This is a number")
                        .SetValueMapper(new IntValueMapper())
                        .SetType(EditorType.Numeric);

                    editor.AddField(x => x.Image)
                        .SetDescription("This is an image");
                });

                listEditorConfig.AddEditor<TestEntityVariantC>(editor =>
                {
                    editor.AddDefaultButton(DefaultButtonType.SaveNew);
                    editor.AddDefaultButton(DefaultButtonType.View);
                    editor.AddDefaultButton(DefaultButtonType.Edit);
                    editor.AddDefaultButton(DefaultButtonType.SaveExisting);
                    editor.AddDefaultButton(DefaultButtonType.Delete);

                    editor.AddField(x => x.Id)
                        .SetDescription("This should be readonly")
                        .SetReadonly();

                    editor.AddField(x => x.Name)
                        .SetDescription("This is a name");

                    editor.AddField(x => x.Description)
                        .SetDescription("This is a description")
                        .SetType(EditorType.TextArea);

                    editor.AddField(x => x.Number)
                        .SetDescription("This is a number")
                        .SetValueMapper(new IntValueMapper())
                        .SetType(EditorType.Numeric);

                    editor.AddField(x => x.Quote)
                        .SetDescription("This is a quote");
                });
            }

            void subCollectionListNodeEditor(SubCollectionListEditorConfig<TestEntity> listEditorConfig)
            {
                // TODO: how to treat sub collection editors? configure here or in the sub collection?

                //listEditorConfig.AddDefaultButton(DefaultButtonType.New);
                //listEditorConfig.SetEditor(editor =>
                //{
                //    editor.AddDefaultButton(DefaultButtonType.View);
                //    editor.AddDefaultButton(DefaultButtonType.SaveNew);
                //    editor.AddDefaultButton(DefaultButtonType.SaveExisting);
                //    editor.AddDefaultButton(DefaultButtonType.Delete);

                //    editor.AddField(x => x.Id)
                //        .SetReadonly();

                //    editor.AddField(x => x.Name);

                //    editor.AddField(x => x.Description)
                //        .SetType(EditorType.TextArea);

                //    editor.AddField(x => x.Number)
                //        .SetValueMapper(new IntValueMapper())
                //        .SetType(EditorType.Numeric);
                //});
            }

            services.AddRapidCMS(root =>
            {
                root.AddCollection<TestEntity>("collection-1", "Simple collection", collection =>
                {
                    collection
                        .SetRepository<RepositoryE>()
                        .SetTreeView("Tree 2", ViewType.Tree, entity => entity.Name)
                        .SetListView(listView)
                        .SetNodeEditor(nodeEditor);
                });

                root.AddCollection<TestEntity>("collection-2", "List editor collection", collection =>
                {
                    collection
                        .SetRepository<RepositoryD>()
                        .SetTreeView("Tree 2", ViewType.List, entity => entity.Name)
                        .SetListEditor(listNodeEditor)
                        .SetNodeEditor(nodeEditor);
                });

                root.AddCollection<TestEntity>("collection-3", "Variant collection", collection =>
                {
                    collection
                        .SetRepository<VariantRepository>()
                        .SetTreeView("Tree 3", ViewType.Tree, entity => entity.Name)
                        .AddEntityVariant<TestEntityVariantA>("Variant A", "align-left")
                        .AddEntityVariant<TestEntityVariantB>("Variant B", "align-center")
                        .AddEntityVariant<TestEntityVariantC>("Variant C", "align-right")
                        .SetListView(listView)
                        .SetNodeEditor(nodeEditorWithPolymorphism);
                });

                root.AddCollection<TestEntity>("collection-4", "Collection with sub collections", collection =>
                {
                    collection
                        .SetRepository<RepositoryA>()
                        .SetTreeView("Tree 1", ViewType.Tree, entity => entity.Name)
                        .SetListView(listView)
                        .SetNodeEditor(nodeEditorWithSubCollection)
                        .AddCollection<TestEntity>("sub-collection-1", "Sub Collection 1", subCollection =>
                        {
                            subCollection
                                .SetRepository<RepositoryB>()
                                .SetTreeView("SubTree1", ViewType.List, entity => entity.Name)
                                .SetListView(listView)

                                // TODO: this sub collection must have a list node editor since its parent uses the editor. but how to hide it?
                                .SetListEditor(listNodeEditor)
                                .SetNodeEditor(nodeEditor);
                        })
                        .AddCollection<TestEntity>("sub-collection-2", "Sub Collection 2", subCollection =>
                        {
                            subCollection
                                .SetRepository<RepositoryC>()
                                .SetTreeView("SubTree2", ViewType.Tree, entity => entity.Name)
                                .SetListEditor(listNodeEditor)
                                .SetNodeEditor(nodeEditor);
                        });
                });

                root.AddCollection<TestEntity>("collection-5", "Collections with variant sub collection", collection =>
                {
                    collection
                        .SetRepository<RepositoryF>()
                        .SetTreeView("Tree 1", ViewType.Tree, entity => entity.Name)
                        .SetListView(listView)
                        .SetNodeEditor(nodeEditorWithPolymorphicSubCollection)
                        .AddCollection<TestEntity>("sub-collection-3", "Sub Collection 3", subCollection =>
                        {
                            subCollection
                                .SetRepository<VariantRepository>()
                                .SetTreeView("SubTree1", ViewType.Tree, entity => entity.Name)
                                .AddEntityVariant<TestEntityVariantA>("Variant A", "align-left")
                                .AddEntityVariant<TestEntityVariantB>("Variant B", "align-center")
                                .AddEntityVariant<TestEntityVariantC>("Variant C", "align-right")
                                .SetListView(listView)
                                .SetListEditor(listNodeEditorWithPolymorphism)
                                .SetNodeEditor(nodeEditorWithPolymorphism);
                        });
                });
            });

            services.AddRazorComponents<App.Startup>();

            // TODO: 
            hacky.ConfigureServices(services);
            //services.AddRazorComponents<RapidCMS.Common.Startup>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // TODO: 
            // app.UseRazorComponents<RapidCMS.Common.Startup>();
            var root = app.ApplicationServices.GetService<Root>();
            root.MaterializeRepositories(app.ApplicationServices);

            app.UseStaticFiles();
            app.UseRazorComponents<App.Startup>();

        }
    }
}
