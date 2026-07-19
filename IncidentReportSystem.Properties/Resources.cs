using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace IncidentReportSystem.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				resourceMan = new ResourceManager("IncidentReportSystem.Properties.Resources", typeof(Resources).Assembly);
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static Bitmap bg_auems_login_frm => (Bitmap)ResourceManager.GetObject("bg_auems_login_frm", resourceCulture);

	internal static Bitmap icon_close_frm => (Bitmap)ResourceManager.GetObject("icon_close_frm", resourceCulture);

	internal static Bitmap icon_full_screen_frm => (Bitmap)ResourceManager.GetObject("icon_full_screen_frm", resourceCulture);

	internal static Bitmap icon_minimize_frm => (Bitmap)ResourceManager.GetObject("icon_minimize_frm", resourceCulture);

	internal static Bitmap icon_windowmode_frm => (Bitmap)ResourceManager.GetObject("icon_windowmode_frm", resourceCulture);

	internal static Bitmap logo_auems_login_frm => (Bitmap)ResourceManager.GetObject("logo_auems_login_frm", resourceCulture);

	internal static Bitmap REpassword___icon => (Bitmap)ResourceManager.GetObject("REpassword___icon", resourceCulture);

	internal static Bitmap REuser___icon => (Bitmap)ResourceManager.GetObject("REuser___icon", resourceCulture);

	internal Resources()
	{
	}
}
