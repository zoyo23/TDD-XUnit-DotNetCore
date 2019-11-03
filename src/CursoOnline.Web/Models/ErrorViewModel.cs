namespace CursoOnline.Web.Models
{
    public class ErrorViewModel
    {
        #region Atributos
        public string RequestId { get; set; }
        #endregion

        #region Métodos
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        #endregion
    }
}
