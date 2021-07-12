package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageButton

class MainActivity15 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main15)

        //back button
        val button_b = findViewById<ImageButton>(R.id.back_imageButton_m_info)
        button_b.setOnClickListener{
            val intent_b = Intent(this, MainActivity9::class.java)
            startActivity(intent_b)
        }
    }
}