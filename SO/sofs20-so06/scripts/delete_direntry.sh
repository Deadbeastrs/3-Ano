source sofs20.sh

deleteDirentry()
{
    helpmsg="create a disk with size 1000 alloc one dir and one file with permission of <<first argument>>, then create <<second argument>> direntries and lastly delete <<third argument>> direntries starting at the beginning";

    case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
        	c 1000
        	f -b

            adet $2 $1
            
            for ((i=1;i<=$3;i++))
            do
                echo -ne "dde\n1\nteste$i\nq\n" | tt -b -q 1;
            done

            numberBlocks=6+($2+15)/16; 
            for ((i=6;i<$numberBlocks;i++))
            do
                echo -ne "s\na\n$i\nq\n" | tt -b -q 1 >> binary.txt
            done

            #binário ^
        	#--------------------------------
            #nosso
            
            c 1000
        	f -b

            adet $2 $1
            
            for ((i=1;i<=$3;i++))
            do
                echo -ne "dde\n1\nteste$i\nq\n" | tt -q 1;
            done

            for ((i=6;i<$numberBlocks;i++))
            do
                echo -ne "s\na\n$i\nq\n" | tt -b -q 1 >> normal.txt 
            done

        	diff normal.txt binary.txt > results.txt
    		echo $'diferences:\n'
			cat results.txt
            ;;
        esac;
}
adet(){
    echo -ne "ai\n2\n$2\nq\n" | tt -b -q 1;
    echo -ne "ai\n1\n$2\nq\n" | tt -b -q 1;

    for ((i=1;i<=$1;i++))
    do
        echo -ne "ade\n1\nteste$i\n2\nq\n" | tt -b -q 1;
    done
}