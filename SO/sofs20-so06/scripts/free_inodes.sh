source sofs20.sh

free_inodes()
{
	helpmsg="free <<third argument>> inodes with permission of <<second argument>>, creates disks with sizes <<first argument>> and compares the first block of inode table";
	
	case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)

            inode_table_length=($3+15)/16;
        	c $1
        	f -b
            ait $2 $3
            for ((i=1;i<=$3;i++))
            do  
                    echo -ne "fi\n$i\nq\n" | tt -b -q 1;
            done
            for ((i=1;i<=1+$inode_table_length;i++))
            do
        	    s -i $i >> binary.txt
        	done

            #binario
            #--------------------------------------------
            #nosso

            c $1
        	f -b
            ait $2 $3
            for ((i=1;i<=$3;i++))
            do
                    echo -ne "fi\n$i\nq\n" | tt -q 1;
            done
            for ((i=1;i<=1+$inode_table_length;i++))
            do
        	    s -i $i >> normal.txt
        	done
        	diff normal.txt binary.txt > results.txt
    		echo $'diferences:\n'
			cat results.txt
            ;;
        esac;
}

ait()
{
    for ((i=0;i<$2;i++))
    do
        echo -ne "ai\n1\n$1\nq\n" | tt -b -q 1;
    done
}