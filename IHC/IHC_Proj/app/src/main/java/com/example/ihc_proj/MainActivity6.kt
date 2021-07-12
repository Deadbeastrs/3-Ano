package com.example.ihc_proj

import android.content.Intent
import android.graphics.Bitmap
import android.net.Uri
import android.os.Bundle
import android.os.Parcelable
import android.widget.ImageButton
import android.widget.ImageView
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity


class MainActivity6 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main6)
        var exists: String? = intent.getStringExtra("imageUri")
        if(exists != null){
            val myUri: Uri = Uri.parse(intent.getStringExtra("imageUri"))

            val image1 = findViewById<ImageView>(R.id.imageView8)
            image1.setImageURI(myUri);
        }
        //receber a mensagem
        val textM = findViewById<TextView>(R.id.textView10);
        val str = intent.getStringExtra("message_key")
        textM.text = str;


        //back button
        val imageButton3 = findViewById<ImageButton>(R.id.back_imageButton_3)
        imageButton3.setOnClickListener{
            val intent1 = Intent(this, MainActivity4::class.java)
            startActivity(intent1)
        }

        //participants_list
        val imageButton2 = findViewById<ImageButton>(R.id.participants_list3)
        imageButton2.setOnClickListener{
            val intent2 = Intent(this, MainActivity8::class.java)
            startActivity(intent2)
        }
    }
}