//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using API_WEB_SAE_6.Models;

//namespace API_WEB_SAE_6.Data
//{
//    public class API_WEB_SAEContext : DbContext
//    {
//        public API_WEB_SAEContext(DbContextOptions<API_WEB_SAEContext> options)
//            : base(options)
//        {
//        }
//        #region Controlador Usuarios
//        public DbSet<Sesiones> Sesiones { get; set; } = default!;
//        public DbSet<Usuarios> Usuarios { get; set; } = default!;

//        public DbSet<Perfiles> Perfiles { get; set; } = default!;
//        #endregion

//        #region Controlador Perfil
//        public DbSet<Funciones> Funciones { get; set; } = default!;
//        public DbSet<FuncionesXPerfiles> FuncionesXPerfiles { get; set; } = default!;
//        #endregion

//        #region Controlador Empleados
//        public DbSet<EmpleadosSAE> EmpleadosSAE { get; set; } = default!;
//        public DbSet<HorariosSAE> HorariosSAE { get; set; } = default!;
//        #endregion

//        #region Controlador JPA
//        public DbSet<EventosSAE> EventosSAE { get; set; } = default!;
//        public DbSet<StandJPA> StandJPA { get; set; } = default!;
//        public DbSet<InteresadosSAE> InteresadosSAE { get; set; } = default!;
//        #endregion

//        #region Controlador Salud
//        public DbSet<Especialidad> Especialidad { get; set; } = default!;
//        public DbSet<EspecialistaMedico> EspecialistaMedico { get; set; } = default!;
//        #endregion
//    }
//}
