function set_viewurl(set_url)
{
	
    var set_tar_url;
    var set_tar_go;
    
    switch(set_url){  
	    //-------------------------------------메뉴1
     	case '1' :
            set_tar_url = "inc.php?inc=sub1";
            //set_tar_go = "1";
            break;
	case '1-1' :
            set_tar_url = "inc.php?inc=sub1-1";
            //set_tar_go = "1";
            break;



     	case '2' :
            set_tar_url = "bbs.php?bbstable=jepumso2&categori11=deskpad";
            //set_tar_go = "1";
            break;
	case '2-1' :
            set_tar_url = "bbs.php?bbstable=jepumso2&categori11=deskgoods";
            //set_tar_go = "1";
            break;
	case '2-2' :
            set_tar_url = "bbs.php?bbstable=jepumso2&categori11=office";
            //set_tar_go = "1";
            break;
	case '2-3' :
            set_tar_url = "bbs.php?bbstable=jepumso2&categori11=mousepad";

            //set_tar_go = "1";
            break;
	case '2-4' :
            set_tar_url = "bbs.php?bbstable=jepumso2&categori11=order";
            //set_tar_go = "1";
            break;
	

     	case '3' :
            set_tar_url = "bbs.php?bbstable=photo2";
            //set_tar_go = "1";
            break;


     	case '4' :
            set_tar_url = "bbs.php?bbstable=online2";
            //set_tar_go = "1";
            break;


     	case '5' :
            set_tar_url = "bbs.php?bbstable=gong2";
            //set_tar_go = "1";
            break;

        case '5-1' :
            set_tar_url = "bbs.php?bbstable=faq2";
            //set_tar_go = "1"; 
            break;
	case '5-2' :
            set_tar_url = "bbs.php?bbstable=qna2";
            //set_tar_go = "1"; 
            break;


	case '7' :
            set_tar_url = "inc.php?inc=boho";
            //set_tar_go = "1"; 
            break;

	case '8' :
            set_tar_url = "inc.php?inc=iyong";
            //set_tar_go = "1"; 
            break;

						
     }


     	
    /*if (set_logid == "" && set_tar_go == "1")
    {
       window.location.href = "doc/login.htm";
    }   
    else
    {*/
		
	   window.location.href = set_tar_url;
    //}	
}

/*
링크사용법

<a href="javascript:set_viewurl('1-1')">링크</a>
<a href="javascript:set_viewurl('1-2')">링크</a>

이런식으로 링크를 건다.플래시일때 효과적임~

*/