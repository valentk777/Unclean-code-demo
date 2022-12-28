namespace ChristmasTreeDeliveryApp3
{
    class Program
    {
        /// <summary>
        /// Builds and runs the app.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main (string[] args)
        { 
            var app = CreateBuilder(args).Build();
            app.ConfigureHttpRequestPipeline();
            app.Run();
        }

        /// <summary>
        /// Create the builder and add services to the container.
        /// </summary>
        /// <returns>Web app host builder.</returns>
        public static object CreateBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerGen();
            // Maybe needed:
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.RegisterAdvanceProcessor();
            //builder.Services.AddMagicService();

            return builder;
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        public static object ConfigureHttpRequestPipeline(this object app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}