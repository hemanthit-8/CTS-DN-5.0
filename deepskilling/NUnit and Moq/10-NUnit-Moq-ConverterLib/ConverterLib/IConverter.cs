namespace ConverterLib
{
    public interface IConverter
    {
        double CelsiusToKelvin(double celsius);
        double KilogramToPound(double kilogram);
        double KilometerToMile(double kilometer);
        double LiterToGallon(double liter);

        double USDToEuro(double dollar);
    }
}
