package com.example.plsmobileapp;

import android.app.Activity;
import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class PopEmergencyWindow extends Activity {

    private static final String fileName = "UserInfo";

    Context context;

    private String phoneNumber = "";
    private String token = "";

    Button proceed;
    Button goBack;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.emergency_window);

        context = this;

        proceed = findViewById(R.id.proceed);
        goBack = findViewById(R.id.goBack);

        DisplayMetrics dm = new DisplayMetrics();
        getWindowManager().getDefaultDisplay().getMetrics(dm);

        int width = dm.widthPixels;
        int height = dm.heightPixels;

        getWindow().setLayout((int)(width*.8),(int)(height*.8));

        String[] userInfo = ReadUserInfo(fileName).split("\n");

        phoneNumber = userInfo[0];
        token = userInfo[1];

        proceed.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                new ChangeUserCondition().execute(phoneNumber,token);
                Toast.makeText(context, "The emergency request is sent", Toast.LENGTH_SHORT).show();
                finish();

            }
        });

        goBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });

    }

    private String ReadUserInfo(String fileName){

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

    private class ChangeUserCondition extends AsyncTask<String,String,String> {
        @Override
        protected String doInBackground(String... inputs) {
            try{
                return SendChangedUserCondition();
            }catch (IOException e ){

                return e.getMessage();
            }
            catch (JSONException ex) {
                return ex.getMessage();
            }
        }
    }

    private String SendChangedUserCondition() throws  IOException,JSONException {
        InputStream is = null;


        try {
            URL url = new URL("https://public-localization-services-mobile.azurewebsites.net/add/user");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);
            conn.setRequestMethod("PUT");

            JSONObject jsonParam = new JSONObject();
            jsonParam.put("PhoneNumber", phoneNumber);
            jsonParam.put("IsInDanger", true);
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

        }finally {

            if (is != null){
                is.close();
            }
            return "Condition Send";
        }
    }
}
