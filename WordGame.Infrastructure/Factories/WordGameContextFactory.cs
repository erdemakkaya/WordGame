using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WordGame.Infrastructure.Data;

public class WordGameContextFactory : IDesignTimeDbContextFactory<WordGameContext>
{
    private readonly IConfiguration _configuration;

     public WordGameContextFactory()
        {
        }
    public WordGameContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public WordGameContext CreateDbContext(string[] args)
    {
		var dataSourceBuilder = new NpgsqlDataSourceBuilder(_configuration.GetConnectionString("DefaultConnection"));
		dataSourceBuilder.EnableDynamicJson();
		var optionsBuilder = new DbContextOptionsBuilder<WordGameContext>();
        optionsBuilder.UseNpgsql(dataSourceBuilder.Build());
        

		return new WordGameContext(optionsBuilder.Options);
    }
}