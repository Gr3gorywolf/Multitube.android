package md5579855c995bdb073d8182511e793544c;


public class Customwebbrowseract_micliente
	extends android.webkit.WebViewClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onUnhandledInputEvent:(Landroid/webkit/WebView;Landroid/view/InputEvent;)V:GetOnUnhandledInputEvent_Landroid_webkit_WebView_Landroid_view_InputEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("App1.Customwebbrowseract+micliente, App1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Customwebbrowseract_micliente.class, __md_methods);
	}


	public Customwebbrowseract_micliente () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Customwebbrowseract_micliente.class)
			mono.android.TypeManager.Activate ("App1.Customwebbrowseract+micliente, App1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public Customwebbrowseract_micliente (android.app.Activity p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == Customwebbrowseract_micliente.class)
			mono.android.TypeManager.Activate ("App1.Customwebbrowseract+micliente, App1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.App.Activity, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onUnhandledInputEvent (android.webkit.WebView p0, android.view.InputEvent p1)
	{
		n_onUnhandledInputEvent (p0, p1);
	}

	private native void n_onUnhandledInputEvent (android.webkit.WebView p0, android.view.InputEvent p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
