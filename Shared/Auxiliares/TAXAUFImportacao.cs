
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
using AmbiStore.Shared.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class SELECT_FROM_TB_TAXA_UF_ttu_
{

    [XmlIgnore]
    private List<TAXA_UF> listaTaxas = new List<TAXA_UF>();
    public List<TAXA_UF> retornaListaTaxas()
    {
        foreach (var item in dATA_RECORD1Field)
        {
            var Rayban = new TAXA_UF();
            Rayban.TAXA_ID = item.ID_CTI;
            Rayban.DESCRICAO = item.DESCRICAO;
            Rayban.BASE_ICMS = decimal.Parse(item.BASE_ICMS == "" ? "0" : item.BASE_ICMS);
            Rayban.BASE_ICMSFE = decimal.Parse(item.BASE_ICMSFE == "" ? "0" : item.BASE_ICMSFE);
            Rayban.BASE_ICMS_ST = decimal.Parse(item.BASE_ICMS_ST == "" ? "0" : item.BASE_ICMS_ST);
            Rayban.UF_AC = decimal.Parse(item.UF_AC == "" ? "0" : item.UF_AC);
            Rayban.UF_AL = decimal.Parse(item.UF_AL == "" ? "0" : item.UF_AL);
            Rayban.UF_AM = decimal.Parse(item.UF_AM == "" ? "0" : item.UF_AM);
            Rayban.UF_AP = decimal.Parse(item.UF_AP == "" ? "0" : item.UF_AP);
            Rayban.UF_BA = decimal.Parse(item.UF_BA == "" ? "0" : item.UF_BA);
            Rayban.UF_CE = decimal.Parse(item.UF_CE == "" ? "0" : item.UF_CE);
            Rayban.UF_DF = decimal.Parse(item.UF_DF == "" ? "0" : item.UF_DF);
            Rayban.UF_ES = decimal.Parse(item.UF_ES == "" ? "0" : item.UF_ES);
            Rayban.UF_GO = decimal.Parse(item.UF_GO == "" ? "0" : item.UF_GO);
            Rayban.UF_MA = decimal.Parse(item.UF_MA == "" ? "0" : item.UF_MA);
            Rayban.UF_MG = decimal.Parse(item.UF_MG == "" ? "0" : item.UF_MG);
            Rayban.UF_MS = decimal.Parse(item.UF_MS == "" ? "0" : item.UF_MS);
            Rayban.UF_MT = decimal.Parse(item.UF_MT == "" ? "0" : item.UF_MT);
            Rayban.UF_PA = decimal.Parse(item.UF_PA == "" ? "0" : item.UF_PA);
            Rayban.UF_PB = decimal.Parse(item.UF_PB == "" ? "0" : item.UF_PB);
            Rayban.UF_PE = decimal.Parse(item.UF_PE == "" ? "0" : item.UF_PE);
            Rayban.UF_PI = decimal.Parse(item.UF_PI == "" ? "0" : item.UF_PI);
            Rayban.UF_PR = decimal.Parse(item.UF_PR == "" ? "0" : item.UF_PR);
            Rayban.UF_RJ = decimal.Parse(item.UF_RJ == "" ? "0" : item.UF_RJ);
            Rayban.UF_RN = decimal.Parse(item.UF_RN == "" ? "0" : item.UF_RN);
            Rayban.UF_RO = decimal.Parse(item.UF_RO == "" ? "0" : item.UF_RO);
            Rayban.UF_RR = decimal.Parse(item.UF_RR == "" ? "0" : item.UF_RR);
            Rayban.UF_RS = decimal.Parse(item.UF_RS == "" ? "0" : item.UF_RS);
            Rayban.UF_SC = decimal.Parse(item.UF_SC == "" ? "0" : item.UF_SC);
            Rayban.UF_SE = decimal.Parse(item.UF_SE == "" ? "0" : item.UF_SE);
            Rayban.UF_SP = decimal.Parse(item.UF_SP == "" ? "0" : item.UF_SP);
            Rayban.UF_TO = decimal.Parse(item.UF_TO == "" ? "0" : item.UF_TO);
            Rayban.BASE_ISS = decimal.Parse(item.BASE_ISS == "" ? "0" : item.BASE_ISS);
            Rayban.ISS = decimal.Parse(item.ISS == "" ? "0" : item.ISS);
            Rayban.POR_DIF = item.POR_DIF;
            listaTaxas.Add(Rayban);
        }
        return listaTaxas;
    }


    private SELECT_FROM_TB_TAXA_UF_ttu_DATA_RECORD1[] dATA_RECORD1Field;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("DATA_RECORD1")]
    public SELECT_FROM_TB_TAXA_UF_ttu_DATA_RECORD1[] DATA_RECORD1
    {
        get
        {
            return this.dATA_RECORD1Field;
        }
        set
        {
            this.dATA_RECORD1Field = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class SELECT_FROM_TB_TAXA_UF_ttu_DATA_RECORD1
{

    private string iD_CTIField;

    private string dESCRICAOField;

    private string bASE_ICMSField;

    private string bASE_ICMSFEField;

    private string bASE_ICMS_STField;

    private string uF_ACField;

    private string uF_ALField;

    private string uF_AMField;

    private string uF_APField;

    private string uF_BAField;

    private string uF_CEField;

    private string uF_DFField;

    private string uF_ESField;

    private string uF_GOField;

    private string uF_MAField;

    private string uF_MGField;

    private string uF_MSField;

    private string uF_MTField;

    private string uF_PAField;

    private string uF_PBField;

    private string uF_PEField;

    private string uF_PIField;

    private string uF_PRField;

    private string uF_RJField;

    private string uF_RNField;

    private string uF_ROField;

    private string uF_RRField;

    private string uF_RSField;

    private string uF_SCField;

    private string uF_SEField;

    private string uF_SPField;

    private string uF_TOField;

    private string bASE_ISSField;

    private string iSSField;

    private decimal pOR_DIFField;

    /// <remarks/>
    public string ID_CTI
    {
        get
        {
            return this.iD_CTIField;
        }
        set
        {
            this.iD_CTIField = value;
        }
    }

    /// <remarks/>
    public string DESCRICAO
    {
        get
        {
            return this.dESCRICAOField;
        }
        set
        {
            this.dESCRICAOField = value;
        }
    }

    /// <remarks/>
    public string BASE_ICMS
    {
        get
        {
            return this.bASE_ICMSField;
        }
        set
        {
            this.bASE_ICMSField = value;
        }
    }

    /// <remarks/>
    public string BASE_ICMSFE
    {
        get
        {
            return this.bASE_ICMSFEField;
        }
        set
        {
            this.bASE_ICMSFEField = value;
        }
    }

    /// <remarks/>
    public string BASE_ICMS_ST
    {
        get
        {
            return this.bASE_ICMS_STField;
        }
        set
        {
            this.bASE_ICMS_STField = value;
        }
    }

    /// <remarks/>
    public string UF_AC
    {
        get
        {
            return this.uF_ACField;
        }
        set
        {
            this.uF_ACField = value;
        }
    }

    /// <remarks/>
    public string UF_AL
    {
        get
        {
            return this.uF_ALField;
        }
        set
        {
            this.uF_ALField = value;
        }
    }

    /// <remarks/>
    public string UF_AM
    {
        get
        {
            return this.uF_AMField;
        }
        set
        {
            this.uF_AMField = value;
        }
    }

    /// <remarks/>
    public string UF_AP
    {
        get
        {
            return this.uF_APField;
        }
        set
        {
            this.uF_APField = value;
        }
    }

    /// <remarks/>
    public string UF_BA
    {
        get
        {
            return this.uF_BAField;
        }
        set
        {
            this.uF_BAField = value;
        }
    }

    /// <remarks/>
    public string UF_CE
    {
        get
        {
            return this.uF_CEField;
        }
        set
        {
            this.uF_CEField = value;
        }
    }

    /// <remarks/>
    public string UF_DF
    {
        get
        {
            return this.uF_DFField;
        }
        set
        {
            this.uF_DFField = value;
        }
    }

    /// <remarks/>
    public string UF_ES
    {
        get
        {
            return this.uF_ESField;
        }
        set
        {
            this.uF_ESField = value;
        }
    }

    /// <remarks/>
    public string UF_GO
    {
        get
        {
            return this.uF_GOField;
        }
        set
        {
            this.uF_GOField = value;
        }
    }

    /// <remarks/>
    public string UF_MA
    {
        get
        {
            return this.uF_MAField;
        }
        set
        {
            this.uF_MAField = value;
        }
    }

    /// <remarks/>
    public string UF_MG
    {
        get
        {
            return this.uF_MGField;
        }
        set
        {
            this.uF_MGField = value;
        }
    }

    /// <remarks/>
    public string UF_MS
    {
        get
        {
            return this.uF_MSField;
        }
        set
        {
            this.uF_MSField = value;
        }
    }

    /// <remarks/>
    public string UF_MT
    {
        get
        {
            return this.uF_MTField;
        }
        set
        {
            this.uF_MTField = value;
        }
    }

    /// <remarks/>
    public string UF_PA
    {
        get
        {
            return this.uF_PAField;
        }
        set
        {
            this.uF_PAField = value;
        }
    }

    /// <remarks/>
    public string UF_PB
    {
        get
        {
            return this.uF_PBField;
        }
        set
        {
            this.uF_PBField = value;
        }
    }

    /// <remarks/>
    public string UF_PE
    {
        get
        {
            return this.uF_PEField;
        }
        set
        {
            this.uF_PEField = value;
        }
    }

    /// <remarks/>
    public string UF_PI
    {
        get
        {
            return this.uF_PIField;
        }
        set
        {
            this.uF_PIField = value;
        }
    }

    /// <remarks/>
    public string UF_PR
    {
        get
        {
            return this.uF_PRField;
        }
        set
        {
            this.uF_PRField = value;
        }
    }

    /// <remarks/>
    public string UF_RJ
    {
        get
        {
            return this.uF_RJField;
        }
        set
        {
            this.uF_RJField = value;
        }
    }

    /// <remarks/>
    public string UF_RN
    {
        get
        {
            return this.uF_RNField;
        }
        set
        {
            this.uF_RNField = value;
        }
    }

    /// <remarks/>
    public string UF_RO
    {
        get
        {
            return this.uF_ROField;
        }
        set
        {
            this.uF_ROField = value;
        }
    }

    /// <remarks/>
    public string UF_RR
    {
        get
        {
            return this.uF_RRField;
        }
        set
        {
            this.uF_RRField = value;
        }
    }

    /// <remarks/>
    public string UF_RS
    {
        get
        {
            return this.uF_RSField;
        }
        set
        {
            this.uF_RSField = value;
        }
    }

    /// <remarks/>
    public string UF_SC
    {
        get
        {
            return this.uF_SCField;
        }
        set
        {
            this.uF_SCField = value;
        }
    }

    /// <remarks/>
    public string UF_SE
    {
        get
        {
            return this.uF_SEField;
        }
        set
        {
            this.uF_SEField = value;
        }
    }

    /// <remarks/>
    public string UF_SP
    {
        get
        {
            return this.uF_SPField;
        }
        set
        {
            this.uF_SPField = value;
        }
    }

    /// <remarks/>
    public string UF_TO
    {
        get
        {
            return this.uF_TOField;
        }
        set
        {
            this.uF_TOField = value;
        }
    }

    /// <remarks/>
    public string BASE_ISS
    {
        get
        {
            return this.bASE_ISSField;
        }
        set
        {
            this.bASE_ISSField = value;
        }
    }

    /// <remarks/>
    public string ISS
    {
        get
        {
            return this.iSSField;
        }
        set
        {
            this.iSSField = value;
        }
    }

    /// <remarks/>
    public decimal POR_DIF
    {
        get
        {
            return this.pOR_DIFField;
        }
        set
        {
            this.pOR_DIFField = value;
        }
    }
}

