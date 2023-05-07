namespace Users.Extensions
{
	public static class MiddlewareExtensionMethod
	{
		public static WebApplication ConfigureMiddleware(this WebApplication app)
		{
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthentication();

			app.UseCors("rikidaniel");

			app.UseAuthorization();

			app.MapControllers();
			return app;
		}
	}
}