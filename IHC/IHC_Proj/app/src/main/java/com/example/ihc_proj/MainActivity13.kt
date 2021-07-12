package com.example.ihc_proj

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import com.google.android.material.snackbar.Snackbar

class MainActivity13 : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main13)


        //cancel button
        val button1_sgif = findViewById<Button>(R.id.cancel_Button_f_sgif)
        button1_sgif.setOnClickListener{
            val intent1 = Intent(this, MainActivity4::class.java)
            startActivity(intent1)
        }

        //share button
        val button2_sgif = findViewById<Button>(R.id.share_Button_sgif)
        button2_sgif.setOnClickListener{
            val intent2 = Intent(this, MainActivity14::class.java)

            //mandar a descrição
            val editTextM = findViewById<EditText>(R.id.editTextTextPersonName2_sgif)
            intent2.putExtra("message_key", editTextM.getText().toString())

            startActivity(intent2)
        }

        //save button
        val buttonS_sgif = findViewById<Button>(R.id.save_Button_sgif)
        buttonS_sgif.setOnClickListener{
            Snackbar.make(findViewById(R.id.myCoordinatorLayout_1_sgif), "Photo saved on device", Snackbar.LENGTH_SHORT).show()
        }
    }
}