using System;
using System.Collections.Generic;
using System.Linq;
using blg_server.Models;
using blg_server.Repositories;

namespace blg_server.Services
{
    public class BlgsService
    {
        private readonly BlgsRepository _repo;
        public BlgsService(BlgsRepository repo)
        {
            _repo = repo;
        }
        internal IEnumerable<Blg> GetByAccountId(string id)
        {
            return _repo.GetByOwnerId(id);
        }
        internal IEnumerable<Blg> GetAll()
        {
         IEnumerable<Blg> blgs = _repo.GetAll();
         return blgs.ToList().FindAll(r => r.IsPublished );
        }
        internal Blg Create(Blg newBlg)
        {
            newBlg.Id = _repo.Create(newBlg);
            return newBlg;
        }
        internal Blg GetById(int id)
        {
            Blg original = _repo.Get(id);
            if (original == null ) { throw new Exception("Invalid Id"); }
            return original;
        }
        internal Blg Edit(Blg editBlg, string userId)
        {
         Blg original = GetById(editBlg.Id);
         if(original.CreatorId != userId)
         {
             throw new Exception("Dont do anything Shiwani!");
         }
         editBlg.Title = editBlg.Title == null ? original.Title : editBlg.Title;
         editBlg.Body = editBlg.Body == null ? original.Body : editBlg.Body;
         editBlg.IsPublished = editBlg.IsPublished== false? original.IsPublished : editBlg.IsPublished;
         return _repo.Edit(editBlg);
        }
        internal string Delete(int id, string userId)
        {
            Blg original = GetById(id);
            if (original.CreatorId != userId)
            {
              throw new Exception("Not for u!");
            }
            _repo.Remove(id);
            return "Deleted!!!";
        }
    }
}