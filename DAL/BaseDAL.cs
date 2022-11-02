using BibliotecaViva.DAO;

namespace DAL 
{
    public abstract class BaseDAL
    {
        public plataformaonlifeContext DataContext { protected get; set; }

        public BaseDAL(plataformaonlifeContext dataContext)
        {
            DataContext = dataContext;
        }
    }
}