using System.Collections.Generic;
using System.Data;
using System.Linq;
using blg_server.Models;
using Dapper;

namespace blg_server.Repositories
{
    public class BlgsRepository
    {
        private readonly IDbConnection _db;
        public BlgsRepository(IDbConnection db)
        {
            _db=db;
        }
        internal IEnumerable<Blg> GetByOwnerId(string id)
        {
            string sql = @"
            SELECT
            blg.*,
            profile.*
            FROM blgs blg
            JOIN profiles profile ON blg.creatorId = profile.id
            WHERE blg.creatorId = @id;
            ";
            return _db.Query<Blg, Profile, Blg>(sql, (blg,profile) =>
            {blg.Creator = profile;
            return blg;
            }, new { id }, splitOn: "id"
            );
        }
    internal IEnumerable<Blg> GetAll()
    {
      string sql = @"
       SELECT 
       blg.*,
       profile.* 
       FROM blgs blg 
       JOIN profiles profile ON blg.creatorId = profile.id;";
      return _db.Query<Blg, Profile, Blg>(sql, (blg, profile) => { blg.Creator = profile; return blg; }, splitOn: "id");
    }

    internal Blg Get(int id)
    {
      string sql = @"
       SELECT 
       blg.*,
       profile.* 
       FROM blgs blg 
       JOIN profiles profile ON blg.creatorId = profile.id
       WHERE blg.id = @id;";
      return _db.Query<Blg, Profile, Blg>(sql, (blg, profile) =>
       { blg.Creator = profile; return blg; }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal int Create(Blg newBlg)
    {
      string sql = @"
      INSERT INTO blgs
      (creatorId, title, body, isPublished)
      VALUES
      (@CreatorId, @Title, @Body, @IsPublished);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newBlg);
    }
    internal Blg Edit(Blg editBlg)
    {
        string sql = @"
        UPDATE blgs
        SET
        title= @Title,
        body= @Body,
        isPublished = @IsPublished
        WHERE id = @Id;
        ";
        _db.Execute(sql, editBlg);
        return editBlg;
    }
    internal void Remove(int id)
    {
        string sql = "DELETE FROM blgs WHERE id= @id LIMIT 1";
        _db.Execute(sql, new { id });
    }
    }
}