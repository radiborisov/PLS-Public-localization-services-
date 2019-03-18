package com.example.plsmobileapp;

        import android.annotation.TargetApi;
        import android.content.Context;
        import android.content.pm.PackageManager;
        import android.location.Criteria;
        import android.location.Location;
        import android.location.LocationListener;
        import android.location.LocationManager;
        import android.os.AsyncTask;
        import android.os.Build;
        import android.os.StrictMode;
        import android.support.v4.app.ActivityCompat;
        import android.support.v7.app.AppCompatActivity;
        import android.os.Bundle;
        import android.util.Log;
        import android.view.View;
        import android.widget.Button;
        import android.widget.EditText;
        import android.widget.TextView;

        import com.google.android.gms.ads.internal.gmsg.HttpClient;
        import com.google.android.gms.location.FusedLocationProviderClient;
        import com.google.android.gms.location.LocationServices;
        import com.google.android.gms.tasks.OnSuccessListener;

        import org.json.JSONException;
        import org.json.JSONObject;
        import org.w3c.dom.Text;

        import java.io.BufferedInputStream;
        import java.io.BufferedReader;
        import java.io.DataOutputStream;
        import java.io.IOException;
        import java.io.InputStream;
        import java.io.InputStreamReader;
        import java.io.OutputStream;
        import java.net.HttpURLConnection;
        import java.net.MalformedURLException;
        import java.net.URL;
        import java.net.URLConnection;
        import java.nio.charset.StandardCharsets;

        import static android.Manifest.permission.ACCESS_FINE_LOCATION;

public class MainActivity extends AppCompatActivity {


     private FusedLocationProviderClient client;

    private static String PhoneNumber = null;
    private static Integer Id = 0;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.register_layout);
        Button registerButton = findViewById(R.id.registerButton);

        client = LocationServices.getFusedLocationProviderClient(this);

        registerButton.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                EditText phoneNumber = findViewById(R.id.editText2);
                PhoneNumber = phoneNumber.getText().toString();

                new SendRegister().execute(PhoneNumber);



                //From here we are calling the second view
                setContentView(R.layout.activity_main);
                requestPermission();
                Button button = findViewById(R.id.button);

                //Waiting for clicking
                button.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        //Getting permission for using ACCESS_FINE_LOCATION

                        if (ActivityCompat.checkSelfPermission(MainActivity.this, ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED) {

                            return;
                        }


                      client.getLastLocation().addOnSuccessListener(MainActivity.this, new OnSuccessListener<Location>() {
                          @Override
                          public void onSuccess(Location location) {
                              if (location != null) {

                                  //Setting the information on textviews
                                  TextView latitude = findViewById(R.id.textView3);
                                  TextView longitude = findViewById(R.id.textView4);

                                  double lat = location.getLatitude();
                                  double lon = location.getLongitude();
                                  double alt = location.getAltitude();

                                  String lati = "" + lat;
                                  String longi = "" + lon;
                                  String alti = "" + alt;

                                  latitude.setText(lati);
                                  longitude.setText(longi);

                                  //Opening post request for the locations
                                  SendPossition sp = new SendPossition();
                                  sp.execute(longi,lati,alti,Id.toString());

                              }

                          }
                      });
                    }
                });
            }
        });
    }


    //Sending register get request
    private static String sendRegistration(String phoneNumber) throws  IOException,JSONException{
        URL url = new URL("https://192.168.0.102/api/values/" + phoneNumber);
        HttpURLConnection connection = (HttpURLConnection) url.openConnection();
        connection.setRequestMethod("GET");
        connection.setDoOutput(true);
        connection.setConnectTimeout(5000);
        connection.setReadTimeout(5000);
        connection.connect();
        BufferedReader rd = new BufferedReader(new InputStreamReader(connection.getInputStream()));
        JSONObject jsonObject = new JSONObject();
        String content = "", line;
        while ((line = rd.readLine()) != null) {
            content += line + "\n";
        }

        JSONObject jsonObject1 = new JSONObject();

        jsonObject.getJSONObject(content);

        int result = jsonObject.getInt("Id");
        Id = result;
        return "successful";
    }



    //Sending location post request
    private static String sendMessage(String lon, String lat, String alt,String Id) throws IOException, JSONException {
        InputStream is = null;
        double longitude = Double.parseDouble(lon);
        double latitude = Double.parseDouble(lat);
        double altitude = Double.parseDouble(alt);
        Integer id = Integer.parseInt(Id);

        try {
            URL url = new URL("https://192.168.0.102/api/values");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);

            JSONObject jsonParam = new JSONObject();
            jsonParam.put("Longitude", longitude);
            jsonParam.put("Latitude", latitude);
            jsonParam.put("Altitude", altitude);
            jsonParam.put("UserId", id);


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

    //we visualize the permission for ACCESS_FINE_LOCATION. If the user click cancel the program will stop
    private void requestPermission() {
        ActivityCompat.requestPermissions(this, new String[]{ACCESS_FINE_LOCATION}, 1);
    }

    //Async class for register get request
    private class SendRegister extends AsyncTask<String,Void,String>{
        @Override
        protected String doInBackground(String... strings) {
            try{
                return sendRegistration(strings[0]);
            }catch (IOException e ){

                return e.getMessage();
            }
            catch (JSONException ex) {
                return ex.getMessage();
            }
        }
    }


    //Async class for location post request
    private class SendPossition extends AsyncTask<String,Void,String>{

        @Override
        protected String doInBackground(String... strings) {
            try{
                return sendMessage(strings[0],strings[1],strings[2],strings[3]);
            }catch (IOException e ){
                return e.getMessage();
            }
            catch (JSONException ex) {
                return ex.getMessage();
            }
        }
    }
}


