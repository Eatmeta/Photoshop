using System.Drawing;

namespace MyPhotoshop.Interfaces
{
    public interface ITransformer<in TParameters>
        where TParameters : IParameters, new()
    {
        void Prepare(Size size, TParameters parameters);
        Size ResultSize { get; set; }
        Point? MapPoint(Point newPoint); 
    }
}