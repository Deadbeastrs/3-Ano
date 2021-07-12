package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageButton
import android.widget.TextView

class MainActivity10 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main10)

        //receber a mensagem
        val textM = findViewById<TextView>(R.id.textView15);
        val str = intent.getStringExtra("message_key")
        textM.text = str;

        //back button
        val imageButton1 = findViewById<ImageButton>(R.id.back_imageButton_1)
        imageButton1.setOnClickListener{
            val intent1 = Intent(this, MainActivity2::class.java)
            startActivity(intent1)
        }

        //participants_list
        val imageButton2 = findViewById<ImageButton>(R.id.participants_list)
        imageButton2.setOnClickListener{
            val intent2 = Intent(this, MainActivity8::class.java)
            startActivity(intent2)
        }

        //send
        val imageButton3 = findViewById<ImageButton>(R.id.imageButton)
        imageButton3.setOnClickListener{
            val intent3 = Intent(this, MainActivity10::class.java)
            startActivity(intent3)
        }
    }
}