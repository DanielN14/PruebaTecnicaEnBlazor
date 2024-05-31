ALTER TABLE test_usuario
ALTER COLUMN NombreUsuario nvarchar(30) COLLATE Latin1_General_CI_AS

SELECT TOP (1000) [IdUsuario]
      ,[NombreUsuario]
      ,[Clave]
      ,[IdRolUsuario]
      ,[Estado]
      ,[Identificacion]
  FROM [UserRegistryApp].[dbo].[test_usuario]

DELETE FROM test_usuario
WHERE IdUsuario = 5

DELETE FROM test_habilidadBlandaXusuario
WHERE IdUsuario = 5

DELETE FROM test_persona
WHERE Identificacion = '123412342'

DELETE FROM test_telefono
WHERE Identificacion = '123412342'

DELETE FROM test_correo
WHERE Identificacion = '123412342'

SELECT * FROM test_telefono

