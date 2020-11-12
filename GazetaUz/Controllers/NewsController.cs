using System.Collections.Generic;
using System.Web.Http;
using GazetaUz.Models;
using System.Linq;
using Newtonsoft.Json;
using System;

namespace GazetaUz.Controllers
{
    public class Db
    {
        private readonly static List<Article> Articles;

        static Db()
        {
            var content = System.IO.File.ReadAllText(@"f:\IT\API\GazetaUz\GazetaUz\FileStorage\db.json");
            Articles = JsonConvert.DeserializeObject<List<Article>>(content);
        }

        private static void Save()
        {
            string content = JsonConvert.SerializeObject(Articles);
            System.IO.File.WriteAllText(@"f:\IT\API\GazetaUz\GazetaUz\FileStorage\db.json", content);
        }


        public static List<Article> FindAll()
        {
            return Articles;
        }

        public static Article FindById(Guid id)
        {
            var arcticle = Db.Articles.FirstOrDefault(x => x.Id == id);
            return arcticle;
        }


        public static Article Add(Article arcticle)
        {
            arcticle.Id = Guid.NewGuid();

            Db.Articles.Add(arcticle);

            Db.Save();

            return arcticle;
        }

        public static Article Update(Article arcticle)
        {
            var dbArticle = Db.Articles.FirstOrDefault(x => x.Id == arcticle.Id);

            dbArticle.Title = arcticle.Title;
            dbArticle.Description = arcticle.Description;

            Db.Save();

            return arcticle;
        }

        public static void Delete(Guid id)
        {
            var arcticle = Db.Articles.FirstOrDefault(x => x.Id == id);
            Db.Articles.Remove(arcticle);
            Db.Save();
        }
    }

    public class NewsController : ApiController
    {

        // GET api/<controller>
        public IEnumerable<Article> Get()
        {
            List<Article> arcticleList = Db.FindAll();
            return arcticleList;
        }

        // GET api/<controller>/5
        public Article Get(Guid id)
        {
            Article arcticle = Db.FindById(id);
            return arcticle;
        }

        // POST api/<controller>
        public Article Post(Article arcticle)
        {
            Db.Add(arcticle);
            return arcticle;
        }

        // PUT api/<controller>/5
        public Article Put(Guid id, Article arcticle)
        {
            arcticle.Id = id;
            arcticle = Db.Update(arcticle);

            return arcticle;
        }

        // DELETE api/<controller>/5
        public void Delete(Guid id)
        {
            Db.Delete(id);
        }
    }
}