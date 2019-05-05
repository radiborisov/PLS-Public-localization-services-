package com.example.plsmobileapp;

import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.plsmobileapp.MainActivity;
import com.example.plsmobileapp.R;

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

public class PhoneVerificationFragment extends Fragment {

    private EditText verificationCode;
    private Button register;
    private static final String fileName = "UserInfo";
    Context context;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.phone_verification, container, false);

        context = ((MainActivity)getActivity()).ReturnContext();

        verificationCode = (EditText) view.findViewById(R.id.verificationCode);
        register = (Button) view.findViewById(R.id.checkVerification);

        register.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast.makeText(getActivity(), "Go to GpsActivity", Toast.LENGTH_SHORT).show();



                String verifyCode = verificationCode.getText().toString();
                String phoneNumber = ReadFile();

                SendVerification sendVerification = new SendVerification();
                sendVerification.execute(phoneNumber, verifyCode);

                Intent intent = new Intent(getActivity(), GpsActivity.class);
                startActivity(intent);
            }
        });

        return view;
    }

    public void SaveToken(String token) {
        String phoneNumber = ReadFile();
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

            return  bufferedReader.readLine();
        }catch (IOException e){
            return "False";
        }



    }

    private class SendVerification extends AsyncTask<String,Void,String> {
        @Override
        protected String doInBackground(String... strings) {
            try{
                return sendVerification(strings[0], strings[1]);
            }catch (IOException e ){

                return e.getMessage();
            }
            catch (JSONException ex) {
                return ex.getMessage();
            }
        }
    }

    private String sendVerification(String phoneNumber, String verificationCode) throws  IOException,JSONException{
        InputStream is = null;


        try {
            URL url = new URL("http://public-localization-services-mobile.azurewebsites.net/add/user");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);

            JSONObject jsonParam = new JSONObject();
            jsonParam.put("PhoneNumber", phoneNumber);
            jsonParam.put("VerificationCode", verificationCode);

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

            if (!response.equals("empty")){
                SaveToken(response);
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
