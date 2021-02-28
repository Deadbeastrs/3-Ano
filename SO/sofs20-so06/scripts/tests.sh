source sofs20.sh
adb ()
{   
    helpmsg="fit «number» «perm»... [ OPTIONS ]\n";
    helpmsg+="  Fill inode table with «number» inodes with «perm»\n";
    helpmsg+="PARAMETERS:\n";
    helpmsg+="  «number»       --- number of inodes\n";
    helpmsg+="  «perm»       --- permission\n";
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
                    echo -ne "adb\nq\n" | tt -q 1;
                done
                break
            ;;
        esac;
    done;
}
teste ()
{   
    helpmsg="fit «number» «perm»... [ OPTIONS ]\n";
    helpmsg+="  Fill inode table with «number» inodes with «perm»\n";
    helpmsg+="PARAMETERS:\n";
    helpmsg+="  «number»       --- number of inodes\n";
    helpmsg+="  «perm»       --- permission\n";
    helpmsg+="OPTIONS:\n";
    helpmsg+="  -h           --- this help";
    while [[ $# -gt 0 ]]; do
        case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
                for i in `seq 2 $1`
                do  
                    echo -ne "ai\n2\n22\nq\n" | tt -q 1;
                    echo -ne "ade\n1\nteste\n$i\nq\n" | tt -q 1;
                done
                break
            ;;
        esac;
    done;
}

adet ()
{   
    helpmsg="fit «number» «perm»... [ OPTIONS ]\n";
    helpmsg+="  Fill inode table with «number» inodes with «perm»\n";
    helpmsg+="PARAMETERS:\n";
    helpmsg+="  «number»       --- number of inodes\n";
    helpmsg+="  «perm»       --- permission\n";
    helpmsg+="OPTIONS:\n";
    helpmsg+="  -h           --- this help";
    while [[ $# -gt 0 ]]; do
        case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
                for i in `seq 0 $1`
                do  
                    echo -ne "ade\n1\nteste$i\n1\nq\n" | tt -q 1;
                done
                break
            ;;
        esac;
    done;
}

fit ()
{   
    helpmsg="fit «number» «perm»... [ OPTIONS ]\n";
    helpmsg+="  Fill inode table with «number» inodes with «perm»\n";
    helpmsg+="PARAMETERS:\n";
    helpmsg+="  «number»       --- number of inodes\n";
    helpmsg+="  «perm»       --- permission\n";
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
                    echo -ne "ai\n1\n$2\nq\n" | tt -q 1;
                done
                break
            ;;
        esac;
    done;
}

ttfi ()
{ 
    helpmsg="ttai «number» ... [ OPTIONS ]\n";
    helpmsg+="  Free 1 or more inodes\n";
    helpmsg+="PARAMETERS:\n";
    helpmsg+="  «number»       --- inode number\n";
    helpmsg+="OPTIONS:\n";
    helpmsg+="  -h           --- this help";
    while [[ $# -gt 0 ]]; do
        case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
                echo -ne "fi\n$1\nq\n" | tt -q 1 ;
                break
            ;;
        esac;
    done;
}

ttfdb ()
{

    helpmsg="ttfdb «number» ... [ OPTIONS ]\n";
    helpmsg+="  Free 1 or more dblocksata\n";
    helpmsg+="PARAMETERS:\n";
    helpmsg+="  «number»       --- datablock number\n";
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
                    echo -ne "fdb\n1\nq\n" | tt -q 1;
                done
                break
            ;;
        esac;
    done;

}
