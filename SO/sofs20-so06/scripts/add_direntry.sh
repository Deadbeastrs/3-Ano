source sofs20.sh

add_direntry()
{

    size=1000;
    perm=777;
    dirs=100;
    dataBlockStart=5;
    helpmsg="create a disk with size $size alloc one dir and one file with permission of $perm, then create $dirs direntries";

    case $1 in 
        "-h")
            InfoMessage "$helpmsg";
            return
        ;;
        *)

        "" > binary.txt 
        c $1
        f -b
        echo -ne "ai\n2\n$perm\nq\n" | tt -b -q 1;
        echo -ne "ai\n1\n$perm\nq\n" | tt -b -q 1;
        for ((i=0;i<$dirs;i++))
        do
            echo -ne "ade\n1\nteste $i\n2\nq\n" | tt -b -q 1;
        done

        for ((i=0;i<=8;i++))
        do
            v=$((i+dataBlockStart));
            s -a $v >> binary.txt
        done

        #binário
        #--------------------------------
        #nosso
        
        "" > normal.txt 
        c $1
        f -b
        echo -ne "ai\n2\n$perm\nq\n" | tt -b -q 1;
        echo -ne "ai\n1\n$perm\nq\n" | tt -b -q 1;
        for ((i=0;i<$dirs;i++))
        do
            echo -ne "ade\n1\nteste $i\n2\nq\n" | tt -q 1;
        done

        for ((i=0;i<=8;i++))
        do
            v=$((i+dataBlockStart));
            s -a $v >> normal.txt
        done

        diff normal.txt binary.txt > results.txt
        echo $'diferences:\n'
        cat results.txt
        ;;
        
        
    esac;
}