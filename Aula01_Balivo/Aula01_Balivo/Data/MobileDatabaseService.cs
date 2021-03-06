using Aula01_Balivo.Data.Dtos;
using Aula01_Balivo.Providers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.Forms;

namespace Aula01_Balivo.Data
{
    sealed class MobileDatabaseService
    {
        private static Lazy<MobileDatabaseService> _Lazy = new Lazy<MobileDatabaseService>(() => new MobileDatabaseService());
        private readonly SQLiteConnection _SQLiteConnection;

        public static MobileDatabaseService Current { get => _Lazy.Value; }
        private MobileDatabaseService()
        {
            var path = DependencyService.Get<IDatabasePathProvider>().GetPath();

            _SQLiteConnection = new SQLiteConnection(path);
            _SQLiteConnection.CreateTable<CepDto>();
        }

        public bool Save<TDto>(TDto dto) where TDto : new()
             => _SQLiteConnection.InsertOrReplace(dto) > 0;

        public TableQuery<TDto> Get<TDto>() where TDto : new() 
            => _SQLiteConnection.Table<TDto>();

        public TDto Get<TDto>(Guid id) where TDto : new() 
            => _SQLiteConnection.Find<TDto>(id);
        public TableQuery<TDto> Get<TDto>(Expression<Func<TDto, bool>> expression) where TDto : new()
            => _SQLiteConnection.Table<TDto>().Where(expression);

    } 
}
