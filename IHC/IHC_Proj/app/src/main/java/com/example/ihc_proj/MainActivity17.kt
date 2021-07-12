package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.EditText
import android.widget.ImageButton

class MainActivity17 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main17)

        //back button
        val imageButton1 = findViewById<ImageButton>(R.id.back_imageButton_pn1c)
        imageButton1.setOnClickListener{
            val intent1 = Intent(this, MainActivity8::class.java)
            startActivity(intent1)
        }

        //send
        val imageButton3 = findViewById<ImageButton>(R.id.send_imageButton_pn1c)
        imageButton3.setOnClickListener{
            val intent3 = Intent(this, MainActivity18::class.java)

            //mandar a mensagem escrita
            val editTextM = findViewById<EditText>(R.id.editTextTextPersonName_pn1c)
            intent3.putExtra("message_key", editTextM.getText().toString())

            startActivity(intent3)
        }
    }
}