package com.example.plsmobileapp;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import javax.net.ssl.HttpsURLConnection;

public class PhoneVerificationActivity extends Activity {

    private String phoneNumber = "";
    private String secretKey = "";

    private TextView textView;

    private EditText verificationCode;
    private Button register;
    private static final String fileName = "UserInfo";
    Context context;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        context = this;

        setContentView(R.layout.phone_verification);


        String[] userInfo = ReadFile().split("\n");

        if (userInfo.length < 2){
                Intent intent = new Intent(context, LoadingScreenForVerification.class);
                startActivity(intent);

            return;
        }

            phoneNumber = userInfo[0];
            secretKey = userInfo[1];

            verificationCode = (EditText)findViewById(R.id.verificationCode);
            register = (Button) findViewById(R.id.checkVerification);
            textView = findViewById(R.id.textView3);

            register.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Toast.makeText(context, "Go to GpsActivity", Toast.LENGTH_SHORT).show();



                    String verifyCode = verificationCode.getText().toString();

                    SendVerification sendVerification = new SendVerification();
                    sendVerification.execute(phoneNumber, verifyCode, secretKey);

                    Intent intent = new Intent(context, GpsActivity.class);
                    startActivity(intent);
                    finish();
                }
            });
    }

    public void SaveToken(String token) {
        String phoneNumber = ReadFile().split("\n")[0];
        String fileContents = phoneNumber + "\n" + token;
        FileOutputStream outputStream;

        try {
            outputStream = context.openFileOutput(fileName, Context.MODE_PRIVATE);
            outputStream.write(fileContents.getBytes());
            outputStream.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private String ReadFile() {

        try{
            FileInputStream fis = context.openFileInput(fileName);
            InputStreamReader isr = new InputStreamReader(fis);
            BufferedReader bufferedReader = new BufferedReader(isr);
            StringBuilder sb = new StringBuilder();
            String line;
            while((line=bufferedReader.readLine()) != null){
                sb.append(line + "\n");
            }

            return  sb.toString().trim();
        }catch (IOException e){
            return "False";
        }



    }

    private class SendVerification extends AsyncTask<String,Void,String> {
        @Override
        protected String doInBackground(String... strings) {
            try{
                return sendVerification(strings[0], strings[1], strings[2]);
            }catch (IOException e ){

                return e.getMessage();
            }
            catch (JSONException ex) {
                return ex.getMessage();
            }
        }
    }

    private String sendVerification(String phoneNumber, String verificationCode, String secretKey) throws  IOException,JSONException{
        InputStream is = null;


        try {
            URL url = new URL("http://public-localization-services-authentication.azurewebsites.net/mobilelogin");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);

            JSONObject jsonParam = new JSONObject();
            jsonParam.put("PhoneNumber", phoneNumber);
            jsonParam.put("VerificationCode", verificationCode);
            jsonParam.put("SecretKey", secretKey);

            Log.i("JSON", jsonParam.toString());
            DataOutputStream os = new DataOutputStream(conn.getOutputStream());
            //os.writeBytes(URLEncoder.encode(jsonParam.toString(), "UTF-8"));
            os.writeBytes(jsonParam.toString());



            os.flush();
            os.close();

            Log.i("STATUS", String.valueOf(conn.getResponseCode()));
            Log.i("MSG", conn.getResponseMessage());

            is = conn.getInputStream();

            int responseCode=conn.getResponseCode();

            String response = "";

            if (responseCode == HttpsURLConnection.HTTP_OK) {
                String line;
                BufferedReader br=new BufferedReader(new InputStreamReader(conn.getInputStream()));
                while ((line=br.readLine()) != null) {
                    response+=line;
                }
            }
            else {
                response="empty";

            }

            if (response.toLowerCase().contains("user is locked")){

            }
            else if (!response.equals("empty")){
                SaveToken(response);
            }else {

            }

            conn.disconnect();

            return "successful";

        }
        finally {
            if (is != null){
                is.close();
            }
        }
    }

}
