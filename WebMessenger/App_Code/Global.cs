using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Global
/// </summary>
public static class Global
{
    static bool _isLoaded;

    public static bool isLoaded
    {
        get { return _isLoaded; }
        set { _isLoaded = value; }
    }

    
}
