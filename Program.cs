using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Data;

namespace BaltaDataAccess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "server=DESKTOP-QGLFPL0\\SQLEXPRESS;database=BALTA;integrated security=true; TrustServerCertificate=True";


            #region  //PARTE COMENTADA FEITA A CONSULTA NO BANCO COM ADO.NET
            //Console.WriteLine("Conectado");
            //conection.Open();

            //using (var command = new SqlCommand())
            //{
            //    command.Connection = conection;
            //    command.CommandType = System.Data.CommandType.Text;
            //    command.CommandText = "Select [Id], [Title] from [category]";

            //    //Forma mais rapida e pura de ler dados no sql com .NET
            //    var reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
            //    }
            //}
            #endregion

            using (var conection = new SqlConnection(connectionString))
            {
                //ListCategories(conection);
                //UpdateCategory(conection);
                //CreateCategory(conection);
                //CreateManyCategory(conection);
                //DeleteCategory(conection);
                //ExecuteProcedure(conection);
                //ExecuteReadProcedure(conection);
                //ExecuteScalar(conection);
                //ReadView(conection);
                //OneToOne(conection);
                //OneToMany(conection);
                //QueryMultiple(conection);
                //SelectIn(conection);
                //Like(conection, "teste");
                Transaction(conection);

            }

        }

        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [ID], [TITLE] FROM [CATEGORY]");
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.ID} - {item.TITLE}");
            }
        }

        static void CreateCategory(SqlConnection connection)
        {

            var category = new Category();
            category.ID = Guid.NewGuid();
            category.TITLE = "Amazon AWS";
            category.URL = "Amazon";
            category.DESCRIPTION = "Categoria Destinada a serviços do AWS";
            category.ORDER = 8;
            category.SUMARRY = "AWS Cloud";
            category.FEATURED = false;

            var insertSql = @"Insert into [Category]  values(@ID,@TITLE,@URL,@SUMARRY,@ORDER,@DESCRIPTION,@FEATURED)";

            var ROWS = connection.Execute(insertSql, new
            {
                category.ID,
                category.TITLE,
                category.URL,
                category.SUMARRY,
                category.ORDER,
                category.DESCRIPTION,
                category.FEATURED
            });
            Console.WriteLine($"{ROWS} LINHAS INSERIDAS");
        }

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [CATEGORY] SET [TITLE]=@TITLE WHERE [ID] = @ID";
            var rows = connection.Execute(updateQuery, new
            {
                ID = new Guid("3C099E35-7E60-4FF8-B085-0B4B90E17495"),
                TITLE = "FRONTE END 2023"
            });

            Console.WriteLine($"{rows} registros atualizados");
        }

        static void DeleteCategory(SqlConnection connection)
        {
            var deleteQuery = "DELETE [CATEGORY] WHERE [ID]=@ID AND [ORDER] = @ORDER";
            var rows = connection.Execute(deleteQuery, new
            {
                ID = new Guid("3C099E35-7E60-4FF8-B085-0B4B90E17495"),
                ORDER = 5
            });

            Console.WriteLine($"{rows} registros atualizados");
        }

        static void CreateManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.ID = Guid.NewGuid();
            category.TITLE = "Amazon AWS";
            category.URL = "Amazon";
            category.DESCRIPTION = "Categoria Destinada a serviços do AWS";
            category.ORDER = 8;
            category.SUMARRY = "AWS Cloud";
            category.FEATURED = false;

            var category2 = new Category();
            category2.ID = Guid.NewGuid();
            category2.TITLE = "Categoria nova";
            category2.URL = "Categoria nova";
            category2.DESCRIPTION = "Categoria nova";
            category2.ORDER = 9;
            category2.SUMARRY = "Categoria nova";
            category2.FEATURED = false;

            var insertSql = @"Insert into [Category]  values(@ID,@TITLE,@URL,@SUMARRY,@ORDER,@DESCRIPTION,@FEATURED)";

            var ROWS = connection.Execute(insertSql, new[]
            {
                new{category.ID,
                category.TITLE,
                category.URL,
                category.SUMARRY,
                category.ORDER,
                category.DESCRIPTION,
                category.FEATURED},

                new{category2.ID,
                category2.TITLE,
                category2.URL,
                category2.SUMARRY,
                category2.ORDER,
                category2.DESCRIPTION,
                category2.FEATURED}

            });
            Console.WriteLine($"{ROWS} LINHAS INSERIDAS");
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "spDeleteStudent";
            var pars = new { StudentId = "3BF8C2BA-12BC-4D68-B8FD-1613D146E6E8" };
            var affectedRow = connection.Execute(
                procedure,
                pars,
                commandType: CommandType.StoredProcedure);

            Console.WriteLine($"{affectedRow} LINHAS DELETADAS");
        }

        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "spGetCoursesByCategory";
            var pars = new { CategoryId = "10A4F684-77FC-401F-BD6E-43D8EADF56AE" };
            var courses = connection.Query(
                procedure,
                pars,
                commandType: CommandType.StoredProcedure);

            foreach (var item in courses)
            {
                Console.WriteLine(item.ID);
                Console.WriteLine(item.TAG);
                Console.WriteLine(item.TITLE);
                Console.WriteLine(item.SUMMARY);
                Console.WriteLine(item.URL);
                Console.WriteLine(item.LEVEL);
                Console.WriteLine(item.FREE);
            }
        }

        static void ExecuteScalar(SqlConnection connection)
        {

            var category = new Category();
            category.TITLE = "Amazon AWS Teste";
            category.URL = "Amazon Teste";
            category.DESCRIPTION = "Categoria Destinada a serviços do AWS Teste";
            category.ORDER = 8;
            category.SUMARRY = "AWS Cloud Teste";
            category.FEATURED = false;

            var insertSql = @"Insert into [Category] OUTPUT inserted.[ID] values(NEWID(),@TITLE,@URL,@SUMARRY,@ORDER,@DESCRIPTION,@FEATURED)";
            //OUTPUT inserted.[ID] - metodo do proprio sql que retornar o ultimo id inserido.

            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                category.TITLE,
                category.URL,
                category.SUMARRY,
                category.ORDER,
                category.DESCRIPTION,
                category.FEATURED
            });
            Console.WriteLine($" A categoria inserida foi a: {id}");
        }

        static void ReadView(SqlConnection connection)
        {
            var sql = "select * from vwcourses";

            var courses = connection.Query(sql);
            foreach (var item in courses)
            {
                Console.WriteLine($"{item.ID} - {item.TITLE}");
            }

        }

        //Consulta com Joins
        static void OneToOne(SqlConnection connection)
        {
            var sql = @"SELECT * FROM [CareerItem] 
                        INNER JOIN [Course] ON [CareerItem].[CourseId] = [Course].[Id]";

            var items = connection.Query<Careeritem, Course, Careeritem>(
                sql,
                (Careeritem, Course) =>
                {
                    Careeritem.COURSE = Course;
                    return Careeritem;
                }, splitOn: "ID");

            foreach (var item in items)
            {
                Console.WriteLine($"{item.TITLE} - CURSO: {item.COURSE.TITLE}");
            }
        }

        static void OneToMany(SqlConnection connection)
        {
            var sql = @"SELECT 
                            [Career].[Id],
                            [Career].[Title],
                            [CareerItem].[CareerId] AS [ID],
                            [CareerItem].[Title]
                        FROM 
                            [Career] 
                        INNER JOIN 
                            [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
                        ORDER BY
                            [Career].[Title]";

            var careers = new List<Career>();
            var items = connection.Query<Career, Careeritem, Career>(
                sql,
                (Career, item) =>
                {
                    var car = careers.Where(x => x.ID == Career.ID).FirstOrDefault();
                    if (car == null)
                    {
                        car = Career;
                        car.ITEMS.Add(item);
                        careers.Add(car);
                    }
                    else
                    {
                        car.ITEMS.Add(item);
                    }

                    return Career;
                }, splitOn: "CareerId");

            foreach (var career in careers)
            {
                Console.WriteLine($"{career.TITLE}");
                foreach (var item in career.ITEMS)
                {
                    Console.WriteLine($"{item.TITLE}");
                }
            }


        }

        static void QueryMultiple(SqlConnection connection)
        {
            var query = "SELECT * FROM [Category]; SELECT * FROM [Course]";

            using (var multi = connection.QueryMultiple(query))
            {
                var categories = multi.Read<Category>();
                var courses = multi.Read<Course>();

                foreach (var item in categories)
                {
                    Console.WriteLine(item.TITLE);
                }

                foreach (var item in courses)
                {
                    Console.WriteLine(item.TITLE);
                }
            }
        }

        static void SelectIn(SqlConnection connection)
        {
            var query = @"select * from CAREER where [ID] in @ID";

            var items = connection.Query<Career>(query, new
            {
                ID = new[]{
                     "2C0D1EE6-693D-4349-80A9-0A944208440D",
                     "BD0887DC-639F-420E-AFD4-A3EEBECA5614"
                }
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.TITLE);
            }
        }

        static void Like(SqlConnection connection, string term)
        {

            var query = @"select * from [Course] where [Title] like @exp";

            var items = connection.Query<Course>(query, new
            {
                exp = $"%{term}%"
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.TITLE);
            }
        }

        static void Transaction(SqlConnection connection)
        {

            var category = new Category();
            category.ID = Guid.NewGuid();
            category.TITLE = "Minha categoria que nao quero";
            category.URL = "Amazon";
            category.DESCRIPTION = "Categoria Destinada a serviços do AWS";
            category.ORDER = 8;
            category.SUMARRY = "AWS Cloud";
            category.FEATURED = false;

            var insertSql = @"Insert into [Category]  values(@ID,@TITLE,@URL,@SUMARRY,@ORDER,@DESCRIPTION,@FEATURED)";

            connection.Open();
            using (var transcation = connection.BeginTransaction())
            {
                var ROWS = connection.Execute(insertSql, new
                {
                    category.ID,
                    category.TITLE,
                    category.URL,
                    category.SUMARRY,
                    category.ORDER,
                    category.DESCRIPTION,
                    category.FEATURED
                }, transcation);

                transcation.Commit();
                //transcation.Rollback();

                Console.WriteLine($"{ROWS} LINHAS INSERIDAS");
            }
        }

    }
}