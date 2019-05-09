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

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.concurrent.ExecutionException;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import javax.net.ssl.HttpsURLConnection;

public class PhoneRegisterFragment extends Fragment {

    private static final String TAG = "PhoneFragment";
    private static final String fileName = "UserInfo";

    private EditText phoneNumber;
    private Button sendSms;

    Context context;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.phone_registration, container, false);

        context = ((MainActivity)getActivity()).ReturnContext();

        if (!IsFileEmpty(fileName)){
            if (IsTheUserVerified()){
                Intent intent = new Intent(getActivity(), GpsActivity.class);
                startActivity(intent);
            }
        }


        phoneNumber = (EditText) view.findViewById(R.id.phonenumber);
        sendSms = (Button) view.findViewById(R.id.register);

        sendSms.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast.makeText(getActivity(), "Going to register fragment",Toast.LENGTH_SHORT).show();

                String phoneNum = phoneNumber.getText().toString().trim();

               // SaveFile(phoneNum);

                SendRegister sendRegister = new SendRegister();
                sendRegister.execute(phoneNum);


                Intent intent = new Intent(context, PhoneVerificationActivity.class);
                startActivity(intent);
            }
        });

        return  view;
    }

    private boolean IsTheUserVerified() {
        String[] userInfo =  ReadFileInfo(fileName).split("\n");

        String userPhoneNumber = userInfo[0];
        String userToken = userInfo[1];

        RequestTask requestTask = new RequestTask();

        try {
            int result = requestTask.execute(userPhoneNumber, userToken).get();

            if (result != 200){
                return false;
            }

        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }



        return true;
    }

    class RequestTask extends AsyncTask<String, Integer, Integer>{

        @Override
        protected Integer doInBackground(String... uri) {
            int  result = 0;
            try {
                URL reqURL = new URL("http://public-localization-services-authentication.azurewebsites.net/mobilelogin/" + uri[0]  + "/" + uri[1]); //the URL we will send the request to
                HttpURLConnection request = (HttpURLConnection)(reqURL.openConnection());
                request.setRequestMethod("GET");
                request.connect();

                result = request.getResponseCode();
            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }


            return result;
        }
    }

    private class SendRegister extends AsyncTask<String,Void,String> {
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

    private String sendRegistration(String phoneNumber) throws  IOException,JSONException{
        InputStream is = null;


        try {
            URL url = new URL("http://public-localization-services-authentication.azurewebsites.net/add/mobileregister");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json;charset=UTF-8");
            conn.setDoOutput(true);
            conn.setDoInput(true);

            JSONObject jsonParam = new JSONObject();
            jsonParam.put("PhoneNumber", phoneNumber);

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
                SaveFile(phoneNumber, response);
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


    private void SaveFile(String phoneNumber, String secretKey) {

        String saveUserInfo = phoneNumber + "\n" + secretKey;
        try {
            FileOutputStream fos = context.openFileOutput(fileName, Context.MODE_PRIVATE);
            fos.write(saveUserInfo.getBytes());
            fos.close();
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    private boolean FileExists(String fileName){
        File file = new File(context.getFilesDir(), fileName);
        return file.exists();
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

    private boolean IsFileEmpty(String fileName){
        String info = "";
        boolean isEmpty = true;
        if (!FileExists(fileName)){
            return isEmpty;
        }

        info = ReadFileInfo(fileName);

        if (info.equals("empty")){
            return true;
        }

        Pattern pattern = Pattern.compile("^[0-9]+\\n");

        Matcher matcher = pattern.matcher(info);

        if (matcher.find()){
            isEmpty = false;
        }else {
            return true;
        }

        Pattern pattern2 = Pattern.compile("\\n(.+)");

        matcher = pattern2.matcher(info);
        if (matcher.find()){
            isEmpty = false;
        }else {
            return true;
        }

        return isEmpty;

    }
}
