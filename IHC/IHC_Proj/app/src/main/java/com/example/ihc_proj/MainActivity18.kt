package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageButton
import android.widget.TextView

class MainActivity18 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main18)

        //receber a mensagem
        val textM = findViewById<TextView>(R.id.textView15_pn1cr);
        val str = intent.getStringExtra("message_key")
        textM.text = str;

        //back button
        val imageButton1 = findViewById<ImageButton>(R.id.back_imageButton_1_pn1cr)
        imageButton1.setOnClickListener{
            val intent1 = Intent(this, MainActivity8::class.java)
            startActivity(intent1)
        }

        //send
        val imageButton3 = findViewById<ImageButton>(R.id.imageButton_pn1cr)
        imageButton3.setOnClickListener{
            val intent3 = Intent(this, MainActivity10::class.java)
            startActivity(intent3)
        }
    }
}