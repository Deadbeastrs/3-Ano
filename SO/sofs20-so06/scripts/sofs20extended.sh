adb ()
{   
    helpmsg="adb «number» [OPTIONS]\n";
    helpmsg+="  Alloc «number» data blocks\n";
    helpmsg+="PARAMETERS:\n";
    helpmsg+="  «number»       --- number of inodes\n";
    helpmsg+="OPTIONS:\n";
    helpmsg+="  -h           --- this help";
    while [[ $# -gt 0 ]]; do
        case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
                for i in `seq 1 $1`
                do  
                    if [$(( $1 % 68 )) == 0]; then
                        echo -ne "rrc\nq\n" | tt -b -q 1;
                    fi;
                    echo -ne "adb\nq\n" | tt -b -q 1;
                done
                break
            ;;
        esac;
    done;
}