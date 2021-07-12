package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.ImageButton
import android.widget.TextView

class MainActivity8 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main8)

        /*
        //receber a mensagem
        val textM = findViewById<TextView>(R.id.text_participant1);
        val str = intent.getStringExtra("message_key")
        textM.text = str;
         */

        //back button
        val button_b = findViewById<ImageButton>(R.id.back_imageButton)
        button_b.setOnClickListener{
            val intent_b = Intent(this, MainActivity7::class.java)
            startActivity(intent_b)
        }

        //DM1 button
        val button_dm1 = findViewById<Button>(R.id.button)
        button_dm1.setOnClickListener{
            val intent_dm1 = Intent(this, MainActivity17::class.java)
            startActivity(intent_dm1)
        }
    }
}