/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ClothShop;

import java.util.Arrays;
import java.util.List;

/**
 *
 * @author krujz
 */
public class Tarolo 
{
    private static Tarolo instance;

    public static Tarolo getInstance() 
    {
        if (instance == null) 
        {
            instance = new Tarolo();
        }
        return instance;
    }

    List<Details> details;
    

    public List<Details> getDetails() 
    {
        return details;
    }

    

    public Tarolo() {
        
        Details egy = new Details("oltony(fekete)","Feher Jeno",100000);
        Details ketto = new Details("Ing(barna)","Zold Zoltan",5000);
        Details harom = new Details("Teli kabat(feher)","Piros Peter",12000);
        Details negy = new Details("ov(fekete)","Kek Karoly",9700);
        Details ot = new Details("Zokni_pakk(fekete)","Fekete Laszlo",1000);
        Details hat = new Details("Kesztyu_pakk(feher)","Barna Bela",900);
        
        details = Arrays.asList(egy,ketto,harom,negy,ot,hat);
    }  
  }
