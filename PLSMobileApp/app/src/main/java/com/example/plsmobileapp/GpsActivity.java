package com.example.plsmobileapp;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;


public class GpsActivity extends Activity implements SensorEventListener {

    private static final String fileName = "UserInfo";

    private Context context;

    private SensorEventListener sensorEventListener;

    private SensorManager senSensorManager;
    private Sensor senAccelerometer;

    private String phoneNumber = "";
    private String token = "";

    private Button saveMeButton;

    private Button startGpsActivityButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        context = this;
        sensorEventListener = this;
        setContentView(R.layout.activity_startgps);

        startGpsActivityButton = findViewById(R.id.startgpsactivity);



        startGpsActivityButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String[] userInfo = ReadFileInfo(fileName).split("\n");

                if(userInfo.length < 2){
                    return;
                }

                setContentView(R.layout.activity_gps);

                saveMeButton = findViewById(R.id.saveMeButton);

                phoneNumber = userInfo[0];
                token = userInfo[1];

                LocationManager locationManager = (LocationManager) context.getSystemService(LOCATION_SERVICE);

                // Define a listener that responds to location updates
                LocationListener locationListener = new LocationListener() {
                    public void onLocationChanged(Location loc) {
                        // Called when a new location is found by the network location provider.
                        String locStr = String.format("%s %f:%f (%f meters)", loc.getProvider(),
                                loc.getLatitude(), loc.getLongitude(), loc.getAccuracy());
                        TextView tvLoc = (TextView) findViewById(R.id.position1);
                        tvLoc.setText(locStr);
                        new SendLocation().execute(loc.getLongitude(), loc.getLatitude(), loc.getAltitude());
                        Log.v("Gibbons", locStr);
                    }

                    public void onStatusChanged(String provider, int status, Bundle extras) {
                        Log.v("Gibbons", "location onStatusChanged() called");
                    }

                    public void onProviderEnabled(String provider) {
                        Log.v("Gibbons", "location onProviderEnabled() called");
                    }

                    public void onProviderDisabled(String provider) {
                        Log.v("Gibbons", "location onProviderDisabled() called");
                    }
                };

                // Register the listener with the Location Manager to receive location updates
                Log.v("Gibbons", "setting location updates from network provider");
                if (ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                    // TODO: Consider calling
                    //    ActivityCompat#requestPermissions
                    // here to request the missing permissions, and then overriding
                    //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
                    //                                          int[] grantResults)
                    // to handle the case where the user grants the permission. See the documentation
                    // for ActivityCompat#requestPermissions for more details.
                    return;
                }
                locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 60000, 0, locationListener);
                Log.v("Gibbons","setting location updates from GPS provider");
                locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 60000, 0, locationListener);


                senSensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
                senAccelerometer = senSensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
                senSensorManager.registerListener(sensorEventListener, senAccelerometer , SensorManager.SENSOR_DELAY_NORMAL);

                saveMeButton.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        Intent intent = new Intent(GpsActivity.this, PopEmergencyWindow.class);
                        startActivity(intent);
                    }
                });
            }
        });
    }

    private String ReadFileInfo(String fileName){

        try {
            BufferedReader inputReader = new BufferedReader(new InputStreamReader(
                    context.openFileInput(fileName)));
            String inputString;
            StringBuffer stringBuffer = new StringBuffer();
            while ((inputString = inputReader.readLine()) != null) {
                stringBuffer.append(inputString + "\n");
            }
            return stringBuffer.toString();
        } catch (IOException e) {
            e.printStackTrace();
            return "empty";
        }
    }

    private class SendLocation extends AsyncTask<Double,Void,String> {
        @Override
        protected String doInBackground(Double... doubles) {
            try{
                return sendLocation(doubles[0], doubles[1], doubles[2]);
            }catch (IOException e ){

                return e.getMessage();
            }
            catch (JSONException ex) {
                return ex.getMessage();
            }
        }
    }

    private String sendLocation(double longitude, double latitude, double altitude) throws  IOException,JSONException{
        InputStream is = null;


        try {
            URL url = new URL("https://public-localization-services-mobile.azurewebsites.net/add/location");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);

            JSONObject jsonParam = new JSONObject();
            jsonParam.put("Longitude", longitude);
            jsonParam.put("Latitude", latitude);
            jsonParam.put("Altitude", altitude);
            jsonParam.put("PhoneNumber", phoneNumber);
            jsonParam.put("Token", token);

            Log.i("JSON", jsonParam.toString());
            DataOutputStream os = new DataOutputStream(conn.getOutputStream());
            //os.writeBytes(URLEncoder.encode(jsonParam.toString(), "UTF-8"));
            os.writeBytes(jsonParam.toString());

            os.flush();
            os.close();

            Log.i("STATUS", String.valueOf(conn.getResponseCode()));
            Log.i("MSG", conn.getResponseMessage());

            is = conn.getInputStream();


            conn.disconnect();

            return "successful";

        }
        finally {
            if (is != null){
                is.close();
            }
        }
    }


    @Override
    public void onSensorChanged(SensorEvent sensorEvent) {
        Sensor mySensor = sensorEvent.sensor;

        if (mySensor.getType() == Sensor.TYPE_ACCELEROMETER) {
            float x = sensorEvent.values[0];
            float y = sensorEvent.values[1];
            float z = sensorEvent.values[2];
            TextView tvLoc = (TextView) findViewById(R.id.position2);
            String xyzStr = String.format("Position: %f:%f %f ", x,y,z);
            tvLoc.setText(xyzStr);
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int i) {

    }

    public String[] GetUserInfo(){
        String[] userInfo = new String[2];

        userInfo[0] = phoneNumber;
        userInfo[1] = token;

        return userInfo;
    }

}