source sofs20.sh

formatTest()
{
	helpmsg="creates disks with sizes <<first argument>>, compares the superblock after beign formated using the binary and the grp format\n";
	case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
        	c $1
        	f -b
        	s -s 0 > binary.txt
        	c $1
        	f
        	s -s 0 > normal.txt
        	diff normal.txt binary.txt >> results.txt
    		echo $'diferences:\n'
			cat results.txt
            ;;
        esac;
    
}
	
