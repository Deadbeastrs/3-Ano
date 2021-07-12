package com.example.ihc_proj

import android.app.Activity
import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.provider.MediaStore
import android.widget.EditText
import android.widget.ImageButton
import androidx.appcompat.app.AppCompatActivity


class MainActivity7 : AppCompatActivity() {
    val PICK_IMAGE = 1
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main7)

        val imagePop = findViewById<ImageButton>(R.id.imageButton3)
        imagePop.setOnClickListener{
            val getIntent = Intent(Intent.ACTION_GET_CONTENT)
            getIntent.type = "image/*"

            val pickIntent =
                Intent(Intent.ACTION_PICK, MediaStore.Images.Media.EXTERNAL_CONTENT_URI)
            pickIntent.type = "image/*"

            val chooserIntent = Intent.createChooser(getIntent, "Select Image")
            chooserIntent.putExtra(Intent.EXTRA_INITIAL_INTENTS, arrayOf(pickIntent))

            startActivityForResult(chooserIntent, PICK_IMAGE)
        }

        //back button
        val imageButton1 = findViewById<ImageButton>(R.id.back_imageButton)
        imageButton1.setOnClickListener{
            val intent1 = Intent(this, MainActivity2::class.java)
            startActivity(intent1)
        }

        //participants_list
        val imageButton2 = findViewById<ImageButton>(R.id.participats_list)
        imageButton2.setOnClickListener{
            val intent2 = Intent(this, MainActivity8::class.java)
            startActivity(intent2)
        }

        //send
        val imageButton3 = findViewById<ImageButton>(R.id.send_imageButton)
        imageButton3.setOnClickListener{
            val intent3 = Intent(this, MainActivity10::class.java)

            //mandar a mensagem escrita
            val editTextM = findViewById<EditText>(R.id.editTextTextPersonName)
            intent3.putExtra("message_key", editTextM.getText().toString())

            startActivity(intent3)
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if (resultCode == RESULT_OK) {
            val selectedImage: Uri? = data?.data
            val intentChange = Intent(this, MainActivity20::class.java)
            intentChange.putExtra("imageUri", selectedImage.toString())
            startActivity(intentChange)
        }

    }

}