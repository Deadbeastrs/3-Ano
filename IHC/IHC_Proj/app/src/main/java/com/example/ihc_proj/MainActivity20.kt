package com.example.ihc_proj

import android.content.Intent
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageButton
import android.widget.ImageView
import android.widget.TextView

class MainActivity20 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main20)
        val myUri: Uri = Uri.parse(intent.getStringExtra("imageUri"))

        val image1 = findViewById<ImageView>(R.id.imageView_G)
        image1.setImageURI(myUri);
        //receber a mensagem
        val textM = findViewById<TextView>(R.id.textView10_G);
        val str = intent.getStringExtra("message_key")
        textM.text = str;


        //back button
        val imageButton3 = findViewById<ImageButton>(R.id.back_imageButton_3_G)
        imageButton3.setOnClickListener{
            val intent1 = Intent(this, MainActivity2::class.java)
            startActivity(intent1)
        }

        //participants_list
        val imageButton2 = findViewById<ImageButton>(R.id.participants_list3_G)
        imageButton2.setOnClickListener{
            val intent2 = Intent(this, MainActivity8::class.java)
            startActivity(intent2)
        }
    }
}