using Users.Repositories;
using Users.Services;

namespace Users.Extensions
{
	public static class ServiceExtenstionMethod
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services)
		{
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddCors(options =>
			{
				options.AddPolicy(name: "rikidaniel", policy =>
				{
					policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
				});
			});

			services.AddSingleton<IUserService, UserService>();
			services.AddSingleton<IMessageService, MessageService>();
			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddSingleton<IMessageRepository, MessageRepository>();
			services.AddSingleton<IRoomService, RoomService>();
			services.AddSingleton<IRoomRepository, RoomRepository>();
			services.AddSingleton<IGameService, GameService>();
			services.AddSingleton<IGameRepository, GameRepository>();
			return services;
		}
	}
}