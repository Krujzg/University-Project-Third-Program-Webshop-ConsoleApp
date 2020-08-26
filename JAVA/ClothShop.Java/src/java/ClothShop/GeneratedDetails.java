/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ClothShop;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author krujz
 */
@XmlRootElement()
public class GeneratedDetails 
{
    @XmlElement
    List<Details> details;

    public GeneratedDetails() {
    }
    
    public GeneratedDetails(String cloth,String vasarlonev,int Ar) 
    {
        this.details = generatedDetails(cloth,vasarlonev,Ar);
    }
    
    private List<Details> generatedDetails(String cloth,String vasarlonev,int Ar ){
        ArrayList<Details> local = new ArrayList<>();
        Random rnd = new Random();
        int numb = 0;

        for (int i = 1; i < 6; i++) {
            numb = rnd.nextInt(Ar);
            Details p1 = new Details(cloth,vasarlonev,numb);
            local.add(p1);
        }
        return local;
    
    }
}
