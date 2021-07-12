package com.example.ihc_proj

import android.content.Intent
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ImageButton
import androidx.navigation.fragment.findNavController

/**
 * A simple [Fragment] subclass as the second destination in the navigation.
 */
class SecondFragment : Fragment() {

    override fun onCreateView(
            inflater: LayoutInflater, container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_second, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        val button_b = view?.findViewById<ImageButton>(R.id.back_imageButton_def11)
        if (button_b != null) {
            Log.d("sss", button_b.toString())
            button_b.setOnClickListener{
                val intent_b = Intent(getActivity(), MainActivity2::class.java)
                startActivity(intent_b)
            }
        }
    }
}
