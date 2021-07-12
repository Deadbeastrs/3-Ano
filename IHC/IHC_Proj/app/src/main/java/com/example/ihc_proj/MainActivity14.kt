package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageButton
import android.widget.TextView

class MainActivity14 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main14)

        //receber a mensagem
        val textM = findViewById<TextView>(R.id.textView10_mgif);
        val str = intent.getStringExtra("message_key")
        textM.text = str;


        //back button
        val imageButton3_mgif = findViewById<ImageButton>(R.id.back_imageButton_3_mgif)
        imageButton3_mgif.setOnClickListener{
            val intent1 = Intent(this, MainActivity4::class.java)
            startActivity(intent1)
        }

        //participants_list
        val imageButton2_mgif = findViewById<ImageButton>(R.id.participants_list3_mgif)
        imageButton2_mgif.setOnClickListener{
            val intent2 = Intent(this, MainActivity8::class.java)
            startActivity(intent2)
        }
    }
}