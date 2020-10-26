namespace MovieDatabase.Model
{
    public class ReportFactory
    {

        public IReport GetReport(string reportType)
        {
            switch (reportType)
            {
                case "PDF":
                    return new PdfReport();
                case "TXT":
                    return new TxtReport();
                default:
                    return null;
                    
            }
        }
    }
}
