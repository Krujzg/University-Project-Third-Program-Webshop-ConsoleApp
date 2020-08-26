package ClothShop;


import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author Krujz
 */



@XmlRootElement
@XmlAccessorType(XmlAccessType.FIELD)
public class Details {

    static final long serialVersionUID = 1L;

    @XmlElement
    String cloth;
    
    @XmlElement
    String vasarlonev;
    
    @XmlElement
    int Ar;
    
    
    
    public Details()
    {
        
    }
    
    public Details(String cloth,String vasarlonev,int Ar)
    {
        this.cloth = cloth;
        this.vasarlonev = vasarlonev;
        this.Ar = Ar;
    }
    
    
    public String getCloth()
    {
        return cloth;
    }
    
    public void setCloth(String cloth)
    {
        this.cloth = cloth;
    }
    
    
    public String getVasarlonev()
    {
        return vasarlonev;
    }
    
    public void setVasarlonev(String vasarlonev)
    {
        this.vasarlonev = vasarlonev;
    }
    
    
    public int getAr()
    {
        return Ar;
    }
    
    public void setAr(int Ar)
    {
        this.Ar = Ar;
    }
}
