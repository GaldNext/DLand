using Dapper;
using DLand.backend.models;
using DLand.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Xml.Linq;

namespace DLand.Utilities
{
    public class Database
    {
        private string _connectionString = "Host = 26.9.63.90; Port = 5432; Database = NEWAladin; Username = postgres; Password = 12345";
        protected async Task<int> GetCount(string table)
        {
            string sql = $"SELECT COUNT(*) FROM {table}";

            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(sql);
        }
        protected async Task<bool> UpdateHero(Hero hero) {
            const string sql1 = "UPDATE DL_HEROES SET Name = @Name, Race = @Race, Class = @Class, Profession = @Profession, Image = @Image WHERE ID = @ID";
            const string sql2 = "UPDATE DL_HEROESPROPERTIES SET Power = @Power, Magit = @Magit, Durability = @Durability, Dexterity = @Dexterity, Wisdom = @Wisdom, Luck = @Luck, Weight = @Weight, Stress = @Stress, HP = @HP, MP = @MP WHERE Character_Id = @Character_Id";
            const string sql3 = "UPDATE DL_HEROESABILITIES SET TypeAbilitie = @TypeAbilitie, NameAbilitie = @NameAbilitie, PersonAbilitie = @PersonAbilitie WHERE ID = @ID AND Character_Id = @Character_Id";

            using var connection = new NpgsqlConnection(_connectionString);
            int rowsAffected = await connection.ExecuteAsync(sql1, new { Name = hero.Name, Race=hero.Race, Class = hero.Class, Profession = hero.Profession, Image = hero.Image, ID = hero.Id});
            rowsAffected += await connection.ExecuteAsync(sql2, new { Power = hero.properties.Power, Magit = hero.properties.Magic, Durability = hero.properties.Durability, Dexterity = hero.properties.Dexterity, Wisdom = hero.properties.Wisdom, Luck = hero.properties.Luck, Weight = hero.properties.Weight, Stress  = hero.properties.Stress, HP  = hero.properties.HP, MP = hero.properties.MP, Character_Id = hero.Id });
            foreach(var heroabilitie in hero.abilities)
            {
                rowsAffected += await connection.ExecuteAsync(sql3, new { TypeAbilitie = heroabilitie.TypeAbilitie, NameAbilitie = heroabilitie.PersonAbilitie[0], PersonAbilitie = heroabilitie.PersonAbilitie[1], ID = heroabilitie.Id, Character_Id = heroabilitie.Character_Id});
            }
            return rowsAffected > 0;
        }
        protected async Task<User?> UserGetById(int userId)
        {
            const string sql = "SELECT * FROM DL_USERS WHERE ID = @Id";

            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = userId });
        }
        protected async Task<User?> UserGetByName(string userName)
        {
            const string sql = "SELECT * FROM DL_USERS WHERE Login = @Login";

            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Login = userName });
        }
        protected async Task<User?> UserGetByNameAndPassword(string userName, string password)
        {
            const string sql = "SELECT * FROM DL_USERS WHERE Login = @Login and Password = @Password";

            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Login = userName, Password = password });
        }
        protected async Task<List<Hero>?> HeroesGetByUserId(int user_id)
        {
            List<Hero> heroes = new List<Hero>() { };
            const string sql = "SELECT * FROM DL_HEROES WHERE User_Id = @User_Id";
            using var connection = new NpgsqlConnection(_connectionString);
            var herold = await connection.QueryAsync<dynamic>(sql, new { User_Id = user_id });
            foreach (var hero in herold) {
                var properties = await HeroPropertyGetById(hero.id);
                var abilities = await HeroAbilitiesGetById(hero.id);
                var items = await HeroItemsGetById(hero.id);
                heroes.Add(new Hero { Id = hero.id, User_Id = hero.user_id, Name = hero.name, Race = hero.race, Class = hero.@class, Profession = hero.profession, OP = hero.op, abilities = abilities, properties = properties, Image = hero.image, items = items });
            }
            return heroes;
        }
        protected async Task<List<Hero>?> HeroesGetAll()
        {
            List<Hero> heroes = new List<Hero>() { };
            const string sql = "SELECT * FROM DL_HEROES";
            using var connection = new NpgsqlConnection(_connectionString);
            var herold = await connection.QueryAsync<dynamic>(sql);
            foreach (var hero in herold)
            {
                var properties = await HeroPropertyGetById(hero.id);
                var abilities = await HeroAbilitiesGetById(hero.id);
                var items = await HeroItemsGetById(hero.id);
                heroes.Add(new Hero { Id = hero.id, User_Id = hero.user_id, Name = hero.name, Race = hero.race, Class = hero.@class, Profession = hero.profession, OP = hero.op, abilities = abilities, properties = properties, Image = hero.image, items = items });
            }
            return heroes;
        }

        protected async Task<Hero?> HeroGetByName(string heroName)
        {
            const string sql = "SELECT * FROM DL_HEROES WHERE Name = @Name";
            using var connection = new NpgsqlConnection(_connectionString);
            var herold = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { Name = heroName });
            var properties = await HeroPropertyGetById(herold.id);
            var abilities = await HeroAbilitiesGetById(herold.id);
            var items = await HeroItemsGetById(herold.id);
            return new Hero { Id = herold.id, User_Id = herold.user_id, Name = herold.name, Race = herold.race, Class = herold.@class, Profession = herold.profession, OP = herold.op, abilities = abilities, properties = properties, Image = herold.image, items = items  };
        }
        protected async Task<Properties?> HeroPropertyGetById(int heroId)
        {
            const string sql = $"SELECT * FROM DL_HEROESPROPERTIES WHERE Character_Id = @Character_Id";
            using var connection = new NpgsqlConnection(_connectionString);
            var pr = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { Character_Id = heroId });
            return new Properties { Dexterity = pr.dexterity, Durability = pr.durability, HP = pr.hp, Luck = pr.luck, Magic = pr.magic, MP = pr.mp, Power = pr.power, Speed = pr.speed, Stress = pr.stress, Weight = pr.weight, Wisdom = pr.wisdom };
        }
        protected async Task<List<HeroAbilities>?> HeroAbilitiesGetById(int heroId)
        {
            const string sql = $"SELECT * FROM DL_HEROESABILITIES WHERE Character_Id = @Character_Id";
            using var connection = new NpgsqlConnection(_connectionString);
            var abilities = await connection.QueryAsync<dynamic>(sql, new { Character_Id = heroId });
            return abilities.Select(x => new HeroAbilities { Character_Id = x.character_Id, Id = x.id, TypeAbilitie=x.typeabilitie, PersonAbilitie = new List<string>{x.nameabilitie, x.personabilitie }}).ToList();
        }
        protected async Task<List<HeroItems>?> HeroItemsGetById(int heroId)
        {
            const string sql = $"SELECT * FROM DL_HEROESITEMS WHERE Character_Id = @Character_Id";
            using var connection = new NpgsqlConnection(_connectionString);
            var items = await connection.QueryAsync<dynamic>(sql, new { Character_Id = heroId });
            return items.Select(x => new HeroItems { Character_Id = x.character_Id, Id = x.id, ItemWeight = x.Weight, Item = new List<string> { x.Name, x.Item } }).ToList();
        }

    }
}
