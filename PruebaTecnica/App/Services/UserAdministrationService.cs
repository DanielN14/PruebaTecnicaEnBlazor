using Microsoft.AspNetCore.Components;
using PruebaTecnica.App.Database;
using PruebaTecnica.App.Models;
using PruebaTecnica.App.Models.DTOs;
using System.Data;
using System.Data.SqlClient;

namespace PruebaTecnica.App.Services;

public class UserAdministrationService
{
    private readonly Conexion _conexion;

    public UserAdministrationService(Conexion conexion)
    {
        _conexion = conexion;
    }

    public object InsertarUsuario(RegistroDTO data)
    {
        try
        {
            SqlParameter[] parametros = PametrosInsertarUsuario(data);
            object resultado = _conexion.GetScalar("InsertarUsuario", parametros);

            return resultado;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<Usuario> EliminarUsuario(int idUsuario)
    {
        try
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@IdUsuario", SqlDbType.Int) { Value = idUsuario });

            DataTable resultado = _conexion.ExecSPWithOutput("EliminarUsuario", parametros.ToArray()).Tables[0];

            return ConvertirSalidaDTAListaUsuarios(resultado);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public SqlParameter[] PametrosInsertarUsuario(RegistroDTO data)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        parametros.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 50) { Value = data.Nombre });
        parametros.Add(new SqlParameter("@PrimerApellido", SqlDbType.VarChar, 50) { Value = data.Apellido1 });
        parametros.Add(new SqlParameter("@SegundoApellido", SqlDbType.VarChar, 50) { Value = data.Apellido2 });
        parametros.Add(new SqlParameter("@IdTipoIdentificacion", SqlDbType.Int) { Value = data.IdTipoIdentificacion });
        parametros.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 20) { Value = data.Identificacion });
        parametros.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 30) { Value = data.NombreUsuario });
        parametros.Add(new SqlParameter("@Clave", SqlDbType.VarChar, 20) { Value = data.Password });
        parametros.Add(new SqlParameter("@CorreoElectronico", SqlDbType.VarChar, 100) { Value = data.Email });
        parametros.Add(new SqlParameter("@IdRolUsuario", SqlDbType.Int) { Value = data.IdRolUsuario });

        SqlParameter telefonosParam = new SqlParameter("@Telefonos", SqlDbType.Structured)
        {
           TypeName = "dbo.Telefonos", 
           Value = ConvertirTelefonosListToDataTable(data.Telefonos)
        };
        parametros.Add(telefonosParam);

        SqlParameter habilidadesBlandasParam = new SqlParameter("@HabilidadesBlandas", SqlDbType.Structured)
        {
            TypeName = "dbo.HabilidadesBlandas",
            Value = ConvertirHabilidadesBlandasListToDataTable(data.HabilidadesBlandas)
        };

        parametros.Add(habilidadesBlandasParam);

        return parametros.ToArray();
    }



    #region ConversionData
    public DataTable ConvertirTelefonosListToDataTable(List<string> lista)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Telefonos", typeof(string));

        foreach (var item in lista)
        {
            dt.Rows.Add(item);
        }

        return dt;
    }
    
    public DataTable ConvertirHabilidadesBlandasListToDataTable(List<int?> lista)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("HabilidadesBlandas", typeof(int)); 

        foreach (var item in lista)
        {
            dt.Rows.Add(item);
        }

        return dt;
    }

    public List<Usuario> ConvertirSalidaDTAListaUsuarios(DataTable dtUsuarios)
    {
        List<Usuario> usuarios = dtUsuarios.AsEnumerable().Select(row =>
                new Usuario
                {
                    IdUsuario = row.Field<int>("IdUsuario"),
                    NombreCompleto = row.Field<string>("NombreCompleto"),
                    Identificacion = row.Field<string>("Identificacion"),
                    Correo = row.Field<string>("CorreoElectronico"),
                    RolUsuario = row.Field<string>("RolUsuario")
                }).ToList();

        return usuarios;
    }

    public RegistroDTO ConvertirSalidaDSAUsuario(DataSet dsUsuarios)
    {
        List<string> telefonos = ConvertirDTTelefonosALista(dsUsuarios.Tables[1]);
        List<int?> habilidadesBlandas = ConvertirDTHabiBlandasALista(dsUsuarios.Tables[2]);

        RegistroDTO usuarios = new RegistroDTO()
        {
            Nombre = dsUsuarios.Tables[0].Rows[0]["Nombre"].ToString(),
            Apellido1 = dsUsuarios.Tables[0].Rows[0]["PrimerApellido"].ToString(),
            Apellido2 = dsUsuarios.Tables[0].Rows[0]["SegundoApellido"].ToString(),
            Identificacion = dsUsuarios.Tables[0].Rows[0]["Identificacion"].ToString(),
            IdRolUsuario = dsUsuarios.Tables[0].Rows[0]["IdRolUsuario"].ToString(),
            IdTipoIdentificacion = dsUsuarios.Tables[0].Rows[0]["IdTipoIdentificacion"].ToString(),
            Email = dsUsuarios.Tables[0].Rows[0]["CorreoElectronico"].ToString(),
            NombreUsuario = dsUsuarios.Tables[0].Rows[0]["NombreUsuario"].ToString(),
            Password = dsUsuarios.Tables[0].Rows[0]["Clave"].ToString(),
            Telefonos = telefonos,
            HabilidadesBlandas = habilidadesBlandas,
        };

        return usuarios;
    }

    private List<string> ConvertirDTTelefonosALista(DataTable dtTelefonos)
    {
        List<string?> listaTelefonos = dtTelefonos.AsEnumerable()
            .Select(row => row["NumeroTelefono"] == DBNull.Value ? null : row.Field<string>("NumeroTelefono"))
            .ToList();

        return listaTelefonos;
    }

    private List<int?> ConvertirDTHabiBlandasALista(DataTable dtIdsHabilidadesB)
    {
        List<int?> listaHabilidadesBlandas = dtIdsHabilidadesB.AsEnumerable()
            .Select(row => row["IdHabilidadBlanda"] == DBNull.Value ? (int?)null : row.Field<int>("IdHabilidadBlanda"))
            .ToList();

        return listaHabilidadesBlandas;
    }
    
    #endregion ConversionData


    #region CargarData
    public List<Usuario> CargarUsuarios()
    {
        try
        {
            DataTable resultado = _conexion.ExecSPWithOutput("ObtenerUsuarios").Tables[0];

            return ConvertirSalidaDTAListaUsuarios(resultado);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public RegistroDTO CargarUsuario(int IdUsuario)
    {
        try
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("@idUsuario", SqlDbType.Int) { Value = IdUsuario });

            DataSet resultados = _conexion.ExecSPWithOutput("ObtenerUsuario", parametros.ToArray());

            return ConvertirSalidaDSAUsuario(resultados);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<TipoIdentificacion> CargarTipoIdentificacion()
    {
        try
        {
            DataTable resultado = _conexion.ExecSPWithOutput("ObtenerTipoIdentificacion").Tables[0];

            List<TipoIdentificacion> tiposIdentificacion = resultado.AsEnumerable().Select(row =>
                new TipoIdentificacion
                {
                    IdTipoIdentificacion = row.Field<int>("IdTipoIdentificacion"),
                    NombreTipoIdentificacion = row.Field<string>("Nombre")
                }).ToList();

            return tiposIdentificacion;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<RolUsuario> CargarRolesUsuario()
    {
        try
        {
            DataTable resultado = _conexion.ExecSPWithOutput("ObtenerRolesUsuario").Tables[0];

            List<RolUsuario> rolesUsuario = resultado.AsEnumerable().Select(row =>
                new RolUsuario
                {
                    IdRolUsuario = row.Field<int>("IdRolUsuario"),
                    NombreRol = row.Field<string>("NombreRol")
                }).ToList();

            return rolesUsuario;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<HabilidadBlanda> CargarHabilidadesBlandas()
    {
        try
        {
            DataTable resultado = _conexion.ExecSPWithOutput("ObtenerCatalogoHabilidadesBlandas").Tables[0];

            List<HabilidadBlanda> habilidadesBlandas = resultado.AsEnumerable().Select(row =>
                new HabilidadBlanda
                {
                    IdHabilidadBlanda = row.Field<int>("IdHabilidadBlanda"),
                    NombreHabilidadBlanda = row.Field<string>("NombreHabilidadBlanda")
                }).ToList();

            return habilidadesBlandas;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
        
    #endregion CargarData
}