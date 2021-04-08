namespace blg_server.Models
{
    public class Blg 
    {
        public int Id {get;set;}
        public string CreatorId {get;set;}
        public string Title {get;set;}
        public string Body {get;set;}
        public bool IsPublished {get;set;}
        public Profile Creator {get;set;}
    }
}