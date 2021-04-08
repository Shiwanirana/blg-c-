namespace blg_server.Models
{
    public class Cmnt 
    {
        public int Id {get;set;}
        public string CreatorId {get;set;}
        public string Body {get;set;}
        public Profile Creator {get;set;}
    }
}