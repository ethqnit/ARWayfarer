using UnityEngine;
using UnityEngine.Android;

public class RequestPermissionScript : MonoBehaviour
{
    public void Start()
    { 
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
            Debug.Log("We have permission to access GPS and Camera");
        else
            Debug.Log("Permission state: " + result);

        // Requesting WRITE_EXTERNAL_STORAGE and CAMERA permissions simultaneously
        //AndroidRuntimePermissions.Permission[] result = AndroidRuntimePermissions.RequestPermissions( "android.permission.WRITE_EXTERNAL_STORAGE", "android.permission.CAMERA" );
        //if( result[0] == AndroidRuntimePermissions.Permission.Granted && result[1] == AndroidRuntimePermissions.Permission.Granted )
        //	Debug.Log( "We have all the permissions!" );
        //else
        //	Debug.Log( "Some permission(s) are not granted..." );


    }
}
   
    