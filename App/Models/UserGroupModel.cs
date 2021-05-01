using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using App.Data;
using App.Services.ExtPokeApis.ApiFactoryBase;
using App.Services.ExtPokeApis.PokeApiCo;
using App.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Models
{
    [Index(nameof(UserId),nameof(GroupId))]
    public class UserGroup
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
