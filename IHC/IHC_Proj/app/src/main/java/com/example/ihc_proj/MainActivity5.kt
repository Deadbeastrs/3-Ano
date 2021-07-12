package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import com.google.android.material.snackbar.Snackbar

class MainActivity5 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main5)

        //cancel button
        val button1 = findViewById<Button>(R.id.cancel_Button_f)
        button1.setOnClickListener{
            val intent1 = Intent(this, MainActivity4::class.java)
            startActivity(intent1)
        }

        //share button
        val button2 = findViewById<Button>(R.id.share_Button)
        button2.setOnClickListener{
            val intent2 = Intent(this, MainActivity6::class.java)

            //mandar a descrição
            val editTextM = findViewById<EditText>(R.id.editTextTextPersonName2)
            intent2.putExtra("message_key", editTextM.getText().toString())

            startActivity(intent2)
        }

        //save button
        val buttonS = findViewById<Button>(R.id.save_Button)
        buttonS.setOnClickListener{
            Snackbar.make(findViewById(R.id.myCoordinatorLayout_1), "Photo saved on device", Snackbar.LENGTH_SHORT).show()
        }
    }
}