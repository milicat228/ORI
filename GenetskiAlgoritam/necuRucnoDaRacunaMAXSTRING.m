function [ MAXSTRING ] = necuRucnoDaRacunaMAXSTRING(pocetak, kraj, preciznost )

x = pocetak: preciznost : kraj;
MAXINT = length(x);
MAXSTRING = log2(MAXINT + 1);

end

