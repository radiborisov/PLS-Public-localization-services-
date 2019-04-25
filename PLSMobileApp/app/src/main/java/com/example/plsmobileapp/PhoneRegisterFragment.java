package com.example.plsmobileapp;

import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Environment;
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
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class PhoneRegisterFragment extends Fragment {

    private static final String TAG = "PhoneFragment";

    private EditText phoneNumber;
    private Button sendSms;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.phone_registration, container, false);

        final Context context = ((MainActivity)getActivity()).ReturnContext();

        phoneNumber = (EditText) view.findViewById(R.id.phonenumber);
        sendSms = (Button) view.findViewById(R.id.register);

        sendSms.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast.makeText(getActivity(), "Going to register fragment",Toast.LENGTH_SHORT).show();

                String phoneNum = phoneNumber.getText().toString();

                SendRegister sendRegister = new SendRegister();
                sendRegister.execute(phoneNum);

                SaveThePhoneNumber(context, phoneNum);

                ((MainActivity)getActivity()).setViewPager(1);
            }
        });

        return  view;
    }

    private String ReadFile(Context context,String fileName) {

        try{
            FileInputStream fis = context.openFileInput("Test.txt");
            InputStreamReader isr = new InputStreamReader(fis);
            BufferedReader bufferedReader = new BufferedReader(isr);
            StringBuilder sb = new StringBuilder();
            String line;
            while ((line = bufferedReader.readLine()) != null) {
                sb.append(line);
            }

            return  bufferedReader.readLine();
        }catch (IOException e){
            return "False";
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
            URL url = new URL("https://public-localization-services.azurewebsites.net/add/phoneverification");
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


            conn.disconnect();

            return "successful";

        }
        finally {
            if (is != null){
                is.close();
            }
        }
    }


    public void SaveThePhoneNumber(Context context, String phoneNumber) {
        String filename = "Test.txt";
        String fileContents = phoneNumber;
        FileOutputStream outputStream;

        try {
            outputStream = context.openFileOutput(filename, Context.MODE_PRIVATE);
            outputStream.write(fileContents.getBytes());
            outputStream.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
