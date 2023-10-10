using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline
{
    public class ManaUtil
    {
        public static int CMC(String manaCost)
        {
            if (manaCost == "0" || manaCost == "")
                return 0;
            List<String> mana = manaCost.Split(',').ToList();
            int cmc = 0;
            foreach (String s in mana)
            {
                if (int.TryParse(s, out int v))
                    cmc += v;
                else
                    cmc += 1;
            }
            return cmc;
        }

        public static List<String> DifferenceOfMana(List<String> cost, List<String> mana)
        {
            cost.Reverse();
            List<String> c = Simulation.CloneList(cost);
            List<String> m = mana;
            foreach (String s in cost)
            {
                if (int.TryParse(s, out int v))
                {
                    int value = v;
                    int indexOfS = c.IndexOf(s);
                    for (int i = 0; i < v; i++)
                    {
                        if (m.Count > 0)
                        {
                            m.RemoveAt(0);
                            value--;
                            if (value == 0)
                                c.Remove((value+1).ToString());
                            //c.RemoveAt(cost.IndexOf(s));
                            else
                            {
                                c[indexOfS] = value.ToString();
                            }
                        }
                    }
                }
                else if (m.Contains(s))
                {
                    m.Remove(s);
                    c.Remove(s);
                }
                else if (m.Contains("O"))
                {
                    m.Remove("O");
                    c.Remove(s);
                }
            }
            return c;
        }

        public static List<String> DifferenceOfMana(List<String> cost, List<String> mana, out List<string> m)
        {
            cost.Reverse();
            List<String> c = Simulation.CloneList(cost);
            m = mana;
            foreach (String s in cost)
            {
                if (int.TryParse(s, out int v))
                {
                    int value = v;
                    int indexOfS = c.IndexOf(s);
                    for (int i = 0; i < v; i++)
                    {
                        if (m.Count > 0)
                        {
                            m.RemoveAt(0);
                            value--;
                            if (value == 0)
                                c.Remove((value + 1).ToString());
                            //c.RemoveAt(cost.IndexOf(s));
                            else
                            {
                                c[indexOfS] = value.ToString();
                            }
                        }
                    }
                }
                else if (m.Contains(s))
                {
                    m.Remove(s);
                    c.Remove(s);
                }
                else if (m.Contains("O"))
                {
                    m.Remove("O");
                    c.Remove(s);
                }
            }
            return c;
        }

        public static List<String> DifferenceOfMana(List<String> cost, String mana)
        {
            return DifferenceOfMana(cost, mana.Split(',').ToList());
        }
    }
}
